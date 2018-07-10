using System;
using MathNet.Numerics;

namespace OESApplication.iOS
{
    public class handleSpectra
    {
        public handleSpectra()
        {
        }
		public float[] getAvgSpectrasColumns(float[,]data, int height, int width )
		{
			float colsummation = 0;
			float[] avgdata = new float[height];
			for (int i = 0; i < height; i++){
				for (int j = 0; j < width; j++){
					colsummation += data[i, j];
				}
				avgdata[i] = colsummation / width;
				Console.WriteLine("avgdata: " +avgdata[i] +" colsummation: "+ colsummation);
				colsummation = 0;
			}

			return avgdata;
		}


		public Tuple<int, float> findPeak(float[] data, int height)
		{         
			float maxVal = 0;
			int peaklocation = -1;
			for (int i = 0; i < height; i++)
			{   
				if (data[i] >maxVal)
				{
					maxVal = data[i];
					peaklocation = i;
				}
			}
			return Tuple.Create(peaklocation, maxVal);
		}


		public double[] CreateWavelenghtToPixelLocationsUsingReferenceSpectra(int bluePeakLocation, int redPeakLocation, int RefLengthanyChannel)
		{
			
			double[] wavelengthArray = new double[RefLengthanyChannel];

			//r = a x + b;
                     
			double[] xdata = new double[] { redPeakLocation, bluePeakLocation };
			double[] ydata = new double[] { 610.65, 449.1 };

            Tuple<double, double> p = Fit.Line(xdata, ydata);
            double b = p.Item1; //  intercept
            double a = p.Item2; //  slope
			Console.WriteLine("wavelengthslope: " + a + "intercept: " + b);

			for (int i = 0; i < RefLengthanyChannel; i++)
            {
                wavelengthArray[i] = i * a + b;
				Console.WriteLine("i "+i+" wavearray: " + wavelengthArray[i]);
            }
            

			return wavelengthArray;
		}

        
		public void normalizeArray(float MaxRef, ref float[] data)
		{
			//float[] normalizedArray = new float[data.Length];

			for (int i = 0; i < data.Length; i++)
            {
				data[i] = data[i] / MaxRef;
            }

			//return normalizedArray;
		}
        
              
		public double calculateIntensity( float[] NormalizedData, double[] wavelengthArray, string channel, int width, float centerWave )
		{
			if (channel=="green"){
				int arg_low=0, arg_high=0;
				double interval = wavelengthArray[1] - wavelengthArray[0];
				double wl_min = (centerWave - width / 2.0);
				double wl_max = (centerWave + width / 2.0);

				for (int i = wavelengthArray.Length-1; i >=0; i--)
				{
					if (wavelengthArray[i] > wl_min)
					{
						arg_low = i;
						Console.WriteLine("arg_low loc: " + arg_low);
						break;
					}
				}
				for (int i = wavelengthArray.Length-1; i >= 0; i--)
                {
					if (wavelengthArray[i] < wl_max)
                    {
						arg_high = i;                  
                    }
                }
				Console.WriteLine(" arg_low: " + arg_low + " arg_high: " + arg_high + " wavelengthArray.Length: "+ wavelengthArray.Length +" NormalizedDataL: "+ NormalizedData.Length);
				Console.WriteLine(" wl_min: " + wl_min + " wl_max: " + wl_max);
				                  
				//double sam_low = (((wl_min - wavelengthArray[arg_low-1]) * (NormalizedData[arg_low + 1]- NormalizedData[arg_low - 1])) / (wavelengthArray[arg_low + 1] - wavelengthArray[arg_low - 1])) + NormalizedData[arg_low-1];

				//double sam_high = (((wl_max - wavelengthArray[arg_high]) * (NormalizedData[arg_high+2] - NormalizedData[arg_high])) / (wavelengthArray[arg_high + 2] - wavelengthArray[arg_high])) + NormalizedData[arg_high];

				double intensity = 0;//sam_low * (wavelengthArray[arg_low] - wl_min) + sam_high * (wl_max - wavelengthArray[arg_high]);            


				for (int i = arg_low; i >= arg_high + 1; i--)
                {
					intensity += (NormalizedData[i]) * (interval);
                }

				return intensity;
			}

			return 0;
		}


		public double measureConcentration(double sampleIntensity, double refIntensity, double slope, double intercept)
		{         
			return  ((-(Math.Log(sampleIntensity/refIntensity)) - intercept )/slope);
		}



    }
}
