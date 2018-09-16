// Copyright (c) 2018 Simi Hartstein
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Sim_Timer.CustomTriggers
{
    public class ControlOrientationTrigger : StateTriggerBase
    {
		private FrameworkElement m_targetElement;

		private double m_currentWidthHeightRatio;

		public double MinWidthHeightRatio { get; set; }

		public double MaxWidthHeightRatio { get; set; }

		public FrameworkElement TargetElement
		{
			get
			{
				return m_targetElement;
			}
			set
			{
				if (m_targetElement != null)
					m_targetElement.SizeChanged -= TargetElement_SizeChanged;

				m_targetElement = value;
				m_targetElement.SizeChanged += TargetElement_SizeChanged;
			}
		}



		private void TargetElement_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			m_currentWidthHeightRatio = e.NewSize.Width / e.NewSize.Height;
			UpdateTrigger();

		}

		private void UpdateTrigger()
		{
			if (m_targetElement == null || (MinWidthHeightRatio <=0 && MaxWidthHeightRatio <= 0))
			{
				SetActive(false);
				return;
			}

			if (MinWidthHeightRatio > 0 && MaxWidthHeightRatio > 0)
				SetActive(MinWidthHeightRatio <= m_currentWidthHeightRatio && m_currentWidthHeightRatio <= MaxWidthHeightRatio);
			else if (MinWidthHeightRatio > 0)
				SetActive(MinWidthHeightRatio <= m_currentWidthHeightRatio);
			else
				SetActive(m_currentWidthHeightRatio <= MaxWidthHeightRatio);
		}
    }
}
