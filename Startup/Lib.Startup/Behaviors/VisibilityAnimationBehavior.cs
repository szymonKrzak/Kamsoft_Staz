using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace Lib.Startup.Behaviors
{
    /// <summary>
    /// Animacja dla zmiany Visibility kontrolki.
    /// </summary>
    public class VisibilityAnimationBehavior : Behavior<Border>
    {
        private Duration _animationDuration = new Duration(new TimeSpan(0, 0, 0, 0, 100));
        private Visibility _initialState = Visibility.Visible;
        private Visibility _hiddenState = Visibility.Collapsed;
        private double _targetHeight = 108;
        private bool animatingHeight = false;
        private bool animatingOpacity = true;

        private DoubleAnimation _heightAnimationOut;
        private DoubleAnimation _opacityAnimationOut;
        private DoubleAnimation _heightAnimationIn;
        private DoubleAnimation _opacityAnimationIn;

        private bool changing = false;

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Duration AnimationDuration
        {
            get { return _animationDuration; }
            set { _animationDuration = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Visibility InitialState
        {
            get { return _initialState; }
            set { _initialState = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Visibility HiddenState
        {
            get { return _hiddenState; }
            set { _hiddenState = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double TargetHeight
        {
            get { return _targetHeight; }
            set { _targetHeight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AnimatingHeight
        {
            get { return animatingHeight; }
            set { animatingHeight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AnimatingOpacity
        {
            get { return animatingOpacity; }
            set { animatingOpacity = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (_initialState == Visibility.Visible)
                AssociatedObject.Height = _targetHeight;
            else
                AssociatedObject.Height = 0;

            _heightAnimationIn = new DoubleAnimation(_targetHeight, AnimationDuration, FillBehavior.HoldEnd);
            _opacityAnimationIn = new DoubleAnimation(1, AnimationDuration, FillBehavior.HoldEnd);
            _heightAnimationOut = new DoubleAnimation(0, AnimationDuration, FillBehavior.HoldEnd);
            _opacityAnimationOut = new DoubleAnimation(0, AnimationDuration, FillBehavior.HoldEnd);
            _heightAnimationOut.Completed += (sender, args) =>
            {
                    changing = true;
                    AssociatedObject.SetCurrentValue(Border.VisibilityProperty, _hiddenState);
            };
            if (!animatingHeight)
            {
                _opacityAnimationOut.Completed += (sender, args) =>
                {
                    changing = true;
                    AssociatedObject.SetCurrentValue(Border.VisibilityProperty, _hiddenState);
                };
            }

            AssociatedObject.SetCurrentValue(Border.VisibilityProperty,
                                             InitialState == Visibility.Collapsed
                                                ? Visibility.Collapsed
                                                : Visibility.Visible);

            AssociatedObject.IsVisibleChanged += (sender, e) => { Updated(sender, e); };
        }

        private void Updated(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (changing)
            {
                changing = false;
                return;
            }
            var value = (Visibility)AssociatedObject.GetValue(Border.VisibilityProperty);
            switch (value)
            {
                case Visibility.Collapsed:
                case Visibility.Hidden:
                    AssociatedObject.SetCurrentValue(Border.VisibilityProperty, Visibility.Visible);
                    if (animatingHeight)
                        AssociatedObject.BeginAnimation(Border.HeightProperty, _heightAnimationOut);
                    if (animatingOpacity)
                        AssociatedObject.BeginAnimation(Border.OpacityProperty, _opacityAnimationOut);
                    break;
                case Visibility.Visible:
                    if (animatingHeight)
                        AssociatedObject.BeginAnimation(Border.HeightProperty, _heightAnimationIn);
                    if (animatingOpacity)
                        AssociatedObject.BeginAnimation(Border.OpacityProperty, _opacityAnimationIn);
                    break;
            }
        }
    }
}
