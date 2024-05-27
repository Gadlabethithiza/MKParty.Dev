using System;
namespace eMKParty.BackOffice.Support.Shared
{
	public static class Validations
	{
		public static bool isSpecificType(string testValue, string testType)
		{
			bool returnVal = false;

			switch(testType)
			{
				case "int":
					int out_INT;
					if(int.TryParse(testValue, out out_INT))
					{
						returnVal = true;
					}
					break;

                case "double":
                    double out_DOUBLE;
                    if (double.TryParse(testValue, out out_DOUBLE))
                    {
                        returnVal = true;
                    }
                    break;


                case "date":
                    DateTime out_DATE;
                    if (DateTime.TryParse(testValue, out out_DATE))
                    {
                        returnVal = true;
                    }
                    break;

                case "bool":
                    bool out_BOOL;
                    if (bool.TryParse(testValue, out out_BOOL))
                    {
                        returnVal = true;
                    }
                    break;
            }


			return returnVal;
		}
	}
}

