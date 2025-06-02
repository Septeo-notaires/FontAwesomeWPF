using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FontAwesomeWPF
{
    /// <summary>
    /// Interaction logic for FontAwesomeIcon.xaml
    /// </summary>
    public partial class FontAwesomeIcon : UserControl
    {
        public FontAwesomeIcon()
        {
            InitializeComponent();
            FontFamily faFont = Application.Current.Resources[FontAwesomeIconStyle.Regular.ToDescription()] as FontFamily;
            FontFamily = faFont;
        }

       public static readonly DependencyProperty IconNameProperty = DependencyProperty.Register(
           "IconName",
           typeof(FontAwesomeIconName),
           typeof(FontAwesomeIcon),
           new PropertyMetadata(FontAwesomeIconName.House, OnIconNameChanged)
       );

       public FontAwesomeIconName IconName
       {
           get => (FontAwesomeIconName)GetValue(IconNameProperty);
           set => SetValue(IconNameProperty, value);
       }

        private static void OnIconNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FontAwesomeIcon iconControl && e.NewValue is FontAwesomeIconName iconName)
            {
                string unicode = string.Format("\\u{0:X8}", iconName);
                iconControl.Icon = unicode;
            }
        }

        public static readonly DependencyProperty IconStyleProperty = DependencyProperty.Register(
            "IconStyle",
            typeof(FontAwesomeIconStyle),
            typeof(FontAwesomeIcon),
            new PropertyMetadata(FontAwesomeIconStyle.Regular, OnIconStyleChanged)
        );

        public FontAwesomeIconStyle IconStyle
        {
            get => (FontAwesomeIconStyle)GetValue(IconStyleProperty);
            set => SetValue(IconStyleProperty, value);
        }

        private static void OnIconStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FontAwesomeIcon iconControl && e.NewValue is FontAwesomeIconStyle iconStyle)
            {
                FontFamily faFont = Application.Current.Resources[iconStyle.ToDescription()] as FontFamily;
                iconControl.FontFamily = faFont;
            }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(FontAwesomeIcon), new PropertyMetadata("\uF015"));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor", typeof(Brush), typeof(FontAwesomeIcon), new PropertyMetadata(Brushes.Black));

        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(FontAwesomeIcon), new PropertyMetadata(24.0));

        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }
    }

    public enum FontAwesomeIconStyle
    {
        [Description("FontAwesomeRegular")]
        Regular,

        [Description("FontAwesomeSolid")]
        Solid,

        [Description("FontAwesomeBrands")]
        Brands
        //Light,
    }
}
