using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace LostMinerLib.Util
{
   public class globalVar
    {
       private static ArrayList gal_line_Date ;
       public static ArrayList GAL_LINE_DATE
       {
           get
           {
               return gal_line_Date;
           }
           set
           {
               gal_line_Date = value;
           }
       }

    }
}
