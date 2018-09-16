// Copyright (c) 2018 Simi Hartstein
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Sim_Timer.Converters
{
	internal class MultiplyConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (double)value * GetDoubleValue(parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return (double)value / GetDoubleValue(parameter);
		}

		private double GetDoubleValue(object parameter)
		{
			double multiplier;
			switch (parameter)
			{
				case double val:
					multiplier = val;
					break;
				case string strVal:
					multiplier = double.Parse(strVal);
					break;
				default:
					throw new ArgumentException();
			}
			return multiplier;
		}
	}
}
