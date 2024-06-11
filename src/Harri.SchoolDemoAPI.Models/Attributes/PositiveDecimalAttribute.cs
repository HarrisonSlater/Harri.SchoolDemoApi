﻿using System.ComponentModel.DataAnnotations;

namespace Harri.SchoolDemoAPI.Models.Attributes
{
    public class PositiveDecimalAttribute : RangeAttribute
    {
        public PositiveDecimalAttribute() : base(0, double.MaxValue)
        {
            ErrorMessage = "Decimal should be positive";
        }
    }
}
