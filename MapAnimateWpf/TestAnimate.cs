using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace MapAnimateWpf
{
    public static class TestAnimate
    {
  
        public static Storyboard CreatRotateAnimal(TimeSpan inputTime,DependencyObject dependencyObject)
        {
            var rStory = new Storyboard();
            rStory.FillBehavior = FillBehavior.Stop;
            var rotateAni = new DoubleAnimation(0, 360, inputTime, FillBehavior.Stop);
            rotateAni.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTargetProperty(rotateAni, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(RotateTransform.Angle)"));
            Storyboard.SetTarget(rotateAni, dependencyObject);
            rStory.Children.Add(rotateAni);
            return rStory;
        }
    }
}
