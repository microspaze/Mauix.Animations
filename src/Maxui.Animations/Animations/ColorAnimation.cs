﻿namespace Maxui.Animations
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The ColorAnimation is designed to animate a transition between colors within a user interface. 
    /// </summary>
    public class ColorAnimation : AnimationBase
    {
        public static readonly BindableProperty ToColorProperty =
            BindableProperty.Create(nameof(ToColor), typeof(Color), typeof(ColorAnimation), Colors.Transparent,
                BindingMode.TwoWay, null);

        public Color ToColor
        {
            get { return (Color)GetValue(ToColorProperty); }
            set { SetValue(ToColorProperty, value); }
        }

        protected override Task BeginAnimation()
        {
            if (Target == null)
            {
                throw new NullReferenceException("Null Target property.");
            }

            var fromColor = Target.BackgroundColor;

            return Task.Run(() =>
            {
#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Target.ColorTo(fromColor, ToColor, c => Target.BackgroundColor = c, Convert.ToUInt32(Duration));
                });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete
            });
        }
    }
}