using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Globalization;

namespace Lib.Startup
{
	public partial class SplashWindow : Form
	{
		#region Member Variables
        // okno
		private static SplashWindow _window = null;
        // wątek
		private static Thread _thread = null;
        // Krok o który zostanie zwiększone/zmniejszone Opacity okna po TIMER_INTERVAL
		private double _opacityStep = .2;
        // Odstęp czasowy po którym zostanie odświeżony progress bar i opacity okna
		private const int TimerIntervalConst = 50;
        // komunikat wyświetlany nad progress barem
		private string _information;
        // pozostały czas
		private string _timeRemaining;
        // Aktualny checkPoint
        private double _currentCheckPoint = 0.0;
        // aktualny prostokąt będący progressBar-em
		private Rectangle _rectangleProgressBar;
        // Poprzedni checkPoint
		private double _lastCheckPoint = 0.0;
        // wielkośc kroku wykonywanego co tick timera TIMER_INTERVAL
		private double _timerInterval = .015;
		private int _checkPointIndex = 1;
        // ilość tików jaka była potrzebna do zakończenia procesu
		private int _totalTickCount = 0;
        // Poprzedni procentowy podział czasu uruchomienia się aplikacji na checkPointy
        private ArrayList _previousCheckPoints;
        // tablica przechowująca odstępy pomiędzy poszczególnymi checkPointami przy aktualnym uruchomieniu
		private ArrayList _actualLaunchTimes = new ArrayList();
        // Czas startu procesu
		private DateTime _processStartTime;
        // Flaga informująca o pierwszym uruchomieniu
		private bool _isFirstLaunched = false;
        // Flaga informująca czy dokonała się już inicjalizacja procesu (zaczytanie poprzednich wartości i ustawienie _processStartTime)
		private bool _isInitialized = false;
        // Application code
        private static string _appCode;
        // Application name
        private static string _appName;
        // setings storage
        private SplashWindowXMLStorage _storage;

		#endregion Member Variables

		/// <summary>
        /// Konstructor włączający timer aktualizujący opacity okna i progress bar w zależności od TIMER_INTERVAL
		/// </summary>
		/// <param name="appCode">kod</param>
		/// <param name="appName">nazwa</param>
        public SplashWindow(string appCode, string appName)
		{
			InitializeComponent();
			this.Opacity = 0.0;
            _appCode = appCode;
            _appName = appName;
            lblAppName.Text = appName;
            _storage = new SplashWindowXMLStorage(appCode);
			UpdateTimer.Interval = TimerIntervalConst;
			UpdateTimer.Start();
			this.ClientSize = this.BackgroundImage.Size;
		}

		#region Public Static Methods

        /// <summary>
        /// Metoda odpalająca splash screen w osobnym wątku
        /// </summary>
        /// <param name="appCode">kod</param>
        /// <param name="appName">nazwa</param>
        static public void ShowSplashScreen(string appCode, string appName)
		{
			if (_window != null)
				return;
            _appCode = appCode;
            _appName = appName;
			_thread = new Thread(new ThreadStart(SplashWindow.ShowForm));
			_thread.IsBackground = true;
			_thread.SetApartmentState(ApartmentState.STA);
			_thread.Start();

			while (_window == null || _window.IsHandleCreated == false)
			{
				System.Threading.Thread.Sleep(TimerIntervalConst);
			}
		}

        /// <summary>
        /// Zamknięcie okna
        /// </summary>
		static public void CloseSplashWindow()
		{
			if (_window != null && _window.IsDisposed == false)
			{
                // Ustawienie kroku przejrzystości dodawanego do Opacity aplikacji na - aby uruchomić animację zamykania okna
                _window._opacityStep = -_window._opacityStep;
			}
			_thread = null;	
			_window = null;
		}

		/// <summary>
		/// Metoda wyświetlająca nową informację i aktualizująca progress bar
		/// </summary>
		/// <param name="information">informacja</param>
		static public void WriteInformation(string information)
		{
			WirteInformation(information, true);
		}

        /// <summary>
        /// Metoda wyświetlająca nową informację i aktualizująca progress bar w zależoności od parametru updateProgress
        /// </summary>
        /// <param name="information">Wyświetlana informacja</param>
        /// <param name="updateProgress">Aktualizacja progresu, podanie false zatrzyma progres do czasu następnej informacji</param>
		static public void WirteInformation(string information, bool updateProgress)
		{
			if (_window == null)
				return;

			_window._information = information;

			if (updateProgress)
				_window.UpdateProgressBar();
		}

        /// <summary>
        /// Metoda ustawiająca checkPoint bez komunikatu
        /// </summary>
		static public void SetCheckPoint()
		{
			if (_window == null)
				return;
			_window.UpdateProgressBar();

		}
		#endregion Public Static Methods

		#region Private Methods

		/// <summary>
		/// Prywatna metoda wykonywana po odpaleniu nowego wątku
		/// </summary>
		static private void ShowForm()
		{
            _window = new SplashWindow(_appCode, _appName);
			Application.Run(_window);
		}

        /// <summary>
        /// Aktualizacja progress baru i ustawienie punktu referencyjnego
        /// </summary>
		private void UpdateProgressBar()
		{
			if (_isInitialized == false)
			{
				_isInitialized = true;
				_processStartTime = DateTime.Now;
                ReadPreviousCheckPoints();
			}
			double dblMilliseconds = ElapsedMilliSeconds();
            //Zapamiętanie czasów wywołania checkPointu
			_actualLaunchTimes.Add(dblMilliseconds);

            // Zapamiętanie poprzedniego checkPoint-a
            _lastCheckPoint = _currentCheckPoint;
            if (_previousCheckPoints != null && _checkPointIndex < _previousCheckPoints.Count)
            {
                // Jako ze w _previousCheckPoints mamy zapamiętaną historię poprzedniego uruchomienia procesu to możemy zasymulować progress bar w taki sposób aby dotarł
                // do następnego checkPointa (którego zapiszemy w _currentCheckPoint) w tym samym czasie co ostatnio
                _currentCheckPoint = (double)_previousCheckPoints[_checkPointIndex++];
            }
            else
                _currentCheckPoint = (_checkPointIndex > 0) ? 1 : 0;
		}

        /// <summary>
        /// Metoda przeliczająca czas jaki upłynął od momentu uruchomienia splash screen-a
        /// </summary>
        /// <returns>czas</returns>
		private double ElapsedMilliSeconds()
		{
			TimeSpan ts = DateTime.Now - _processStartTime;
			return ts.TotalMilliseconds;
		}

        /// <summary>
        /// Metoda pobierająca zapamiętane checkPointy z poprzedniego uruchomienia
        /// Poprzednie uruchomienia są zapamiętywne w pliku XML i zapisane w katalogu aplikacji C:\Users\[user]\AppData\Roaming\[app]
        /// </summary>
		private void ReadPreviousCheckPoints()
		{
            // Wyciągnięcie z xml-a zapamiętanego interwału
            string interval = _storage.Interval;
			double dInterval;

			if (Double.TryParse(interval, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out dInterval) == true)
				_timerInterval = dInterval;
			else
				_timerInterval = .0015;

            // Wyciągnięcie z xml-a zapamiętanego podziału pomiędzy checkPointami
            string previousPercents = _storage.CheckPoints;

            if (previousPercents != string.Empty)
			{
                string[] per = previousPercents.Split(null);
                _previousCheckPoints = new ArrayList();

				for (int i = 0; i < per.Length; i++)
				{
					double dblVal;
					if (Double.TryParse(per[i], NumberStyles.Float, NumberFormatInfo.InvariantInfo, out dblVal) == true)
                        _previousCheckPoints.Add(dblVal);
					else
                        _previousCheckPoints.Add(1.0);
				}
			}
			else
			{
				_isFirstLaunched = true;
                _timeRemaining = string.Empty;
			}
		}

        /// <summary>
        /// Metoda zapisująca informacje o aktualnym uruchomieniu w pliku XML katalogu aplikacji C:\Users\[user]\AppData\Roaming\[app]
        /// </summary>
		private void StoreIncrements()
		{
			string sPercent = string.Empty;
			double dblElapsedMilliseconds = ElapsedMilliSeconds();
			for (int i = 0; i < _actualLaunchTimes.Count; i++)
				sPercent += ((double)_actualLaunchTimes[i] / dblElapsedMilliseconds).ToString("0.####", NumberFormatInfo.InvariantInfo) + " ";

            _storage.CheckPoints = sPercent;

			_timerInterval = 1.0 / (double)_totalTickCount;

            _storage.Interval = _timerInterval.ToString("#.000000", System.Globalization.NumberFormatInfo.InvariantInfo);
		}

		public static SplashWindow GetSplashScreen()
		{
			return _window;
		}

		#endregion Private Methods

		#region Event Handlers

        /// <summary>
        /// Obsługa animacji pojawienia się okienka + odrysowanie progress bar-u
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">argumenty</param>
		private void UpdateTimer_Tick(object sender, System.EventArgs e)
		{
			lblStatus.Text = _information;

			// Przeliczenie opacity
            if (_opacityStep > 0)
			{
                // Start splash-a
				_totalTickCount++;
				if (this.Opacity < 1)
                    this.Opacity += _opacityStep;
			}
			else
			{
                // Zamykanie splash-a
				if (this.Opacity > 0)
                    this.Opacity += _opacityStep;
				else
				{
					StoreIncrements();
					UpdateTimer.Stop();
					this.Close();
				}
			}

			// Odrysowanie progress bar-a
            if (_isFirstLaunched == false && _lastCheckPoint < _currentCheckPoint)
			{
                // W _lastCheckPoint mamy zapisaną wartość z wystąpnienia poprzedniego zdarzenia aktualizacji progressBar-a
                // W _currentCheckPoint mamy wartość szacowanego następnego checkPointa dlatego też jesteśmy w stanie zasymulować progres pomiędzy tymi punttami
                // Jeśli aktualne uruchomienie będzie wolniejsze od poprzedniego to progres zaczeka
				_lastCheckPoint += _timerInterval;
				int width = (int)Math.Floor(pnlStatus.ClientRectangle.Width * _lastCheckPoint);
				int height = pnlStatus.ClientRectangle.Height;
				int x = pnlStatus.ClientRectangle.X;
				int y = pnlStatus.ClientRectangle.Y;
				if (width > 0 && height > 0)
				{
					_rectangleProgressBar = new Rectangle(x, y, width, height);
					if (!pnlStatus.IsDisposed)
					{
                        // Przerysowanie prostokącika
						Graphics g = pnlStatus.CreateGraphics();
                        LinearGradientBrush brBackground = new LinearGradientBrush(_rectangleProgressBar, ColorTranslator.FromHtml("#5C7DA5"), ColorTranslator.FromHtml("#285C9E"), LinearGradientMode.Horizontal);
						g.FillRectangle(brBackground, _rectangleProgressBar);
						g.Dispose();
					}
                    // wyliczenie ilości sekund w celu wyświetlenia tej informacji uzytkownikowi
					int secondsLeft = 1 + (int)(TimerIntervalConst * ((1.0 - _lastCheckPoint) / _timerInterval)) / 1000;
                    // Budowa komunikatu z ilością sekund posostałą do zakończenia
                    if(secondsLeft > 1 && secondsLeft < 5)
                        _timeRemaining = string.Format("Pozostały {0} sekundy", secondsLeft);
                    else
                        _timeRemaining = (secondsLeft == 1) ? string.Format("Pozostała 1 sekunda") : string.Format("Pozostało {0} sekund", secondsLeft);
				}
			}
            lblTimeRemaining.Text = _timeRemaining;
		}

		/// <summary>
		/// Obsługa zamknięcia okna po dwukliku
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">args</param>
		private void SplashScreen_DoubleClick(object sender, System.EventArgs e)
		{
			CloseSplashWindow();
		}

		#endregion Event Handlers

        private void SplashWindow_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void SplashWindow_Shown(object sender, EventArgs e)
        {
            this.Activate();
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}