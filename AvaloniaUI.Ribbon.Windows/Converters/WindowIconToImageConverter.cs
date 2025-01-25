using System;
using System.Globalization;
using System.IO;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace AvaloniaUI.Ribbon.Windows.Converters;

public class WindowIconToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null)
        {
            var wIcon = value as WindowIcon;
            var stream = new MemoryStream();
            wIcon.Save(stream);
            stream.Position = 0;
            try
            {
                var bitmap = new Bitmap(stream);
                return bitmap;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}