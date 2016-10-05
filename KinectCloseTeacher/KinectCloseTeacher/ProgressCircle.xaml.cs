using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectCloseTeacher
{
    /// <summary>
    /// ProgressCircle.xaml 的互動邏輯
    /// </summary>
    public partial class ProgressCircle : UserControl
    {
        public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(ProgressCircle));
        public Brush IndicatorBrush
        {
            get { return (Brush)this.GetValue(IndicatorBrushProperty); }
            set { this.SetValue(IndicatorBrushProperty, value); }
        }
        public static readonly DependencyProperty BackgroundBrushProperty = DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(ProgressCircle));
        public Brush BackgroundBrush
        {
            get { return (Brush)this.GetValue(BackgroundBrushProperty); }
            set { this.SetValue(BackgroundBrushProperty, value); }
        }
        public static readonly DependencyProperty ProgressBorderBrushProperty = DependencyProperty.Register("ProgressBorderBrush", typeof(Brush), typeof(ProgressCircle));
        public Brush ProgressBorderBrush
        {
            get { return (Brush)this.GetValue(ProgressBorderBrushProperty); }
            set { this.SetValue(ProgressBorderBrushProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(ProgressCircle));
        public int Value
        {
            get { return (int)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }
        public ProgressCircle()
        {
            InitializeComponent();
        }
    }
    [ValueConversion(typeof(int), typeof(double))]
    public class ValueToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (double)(((int)value * 0.01) * 360);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)(((double)value / 360)) * 100;
        }
    }
}
