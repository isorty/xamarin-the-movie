using System;
using System.Globalization;
using Xamarin.Forms;

namespace TheMovie.Converters
{
	public class ItemTappedEventArgsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
            if (!(value is ItemTappedEventArgs itemTappedEventArgs))
            {
                throw new ArgumentException($"Expected value to be of type {nameof(ItemTappedEventArgs)}", nameof(value));
            }
            return itemTappedEventArgs.Item;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
