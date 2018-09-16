// Copyright (c) 2018 Simi Hartstein
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Sim_Timer.Controls
{
	public sealed partial class NumericTextBox : TextBox
	{

		private string m_oldText;

		private int m_maxNumber;

		public int MaxNumber
		{
			get
			{
				return m_maxNumber;
			}
			set
			{
				m_maxNumber = value;
				MaxLength = MaxNumber.ToString().Length;
				Text = new string('0', MaxLength);
			}
		}

		new int MaxLength
		{
			get { return base.MaxLength - 1; }
			set { base.MaxLength = value + 1; }
		}

		public int Value
		{
			get
			{
				return int.Parse(Text);
			}
			set
			{
				Text = value.ToString();
			}
		}

        public NumericTextBox()
        {
            InitializeComponent();
			m_oldText = Text;
        }

		private void TextBox_GotFocus(object sender, RoutedEventArgs e)
		{
			SetSelection();
		}

		private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			SetSelection();
		}

		private void SetSelection()
		{

			if (SelectionLength != 0)
				return;
			
			if (SelectionStart != MaxLength)
				SelectionLength = 1;
		}

		private void TextBox_TextChanging(TextBox sender=null, TextBoxTextChangingEventArgs args=null)
		{
			var caret = SelectionStart;

			bool isBadText = !Text.All(c => char.IsNumber(c));
			if(isBadText)
			{
				Text = m_oldText;
				SelectionStart = Math.Max(0, caret - 1);
				return;
			}

			if(Text.Length == 0)
			{
				Text = new string('0', MaxLength);
				m_oldText = Text;
				return;
			}

			// if character was deleted
			if (Text.Length < MaxLength)
			{
				Text = new string('0', MaxLength - Text.Length) + Text;
			}
			else if(Text.Length > MaxLength)
			{
				Text = Text.Substring(1);
			}

			SelectionStart = caret;

			int value = int.Parse(Text);
			if (value > MaxNumber)
				Text = MaxNumber.ToString();
			m_oldText = Text;
			return;
		}

		private void TextBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == Windows.System.VirtualKey.Left)
			{
				if (SelectionStart != 0)
					SelectionStart--;
				SelectionLength = 1;
			}
		}
		
	}
}
