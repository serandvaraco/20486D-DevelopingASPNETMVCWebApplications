﻿using ButterfliesShop.Models;
using ButterfliesShop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ButterfliesShop.Validators
{
    public class MaxButterflyQuantityValidation : ValidationAttribute
    {
        private int _maxAmount;
        public MaxButterflyQuantityValidation(int maxAmount)
        {
            _maxAmount = maxAmount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (IButterfliesQuantityService)validationContext.GetService(typeof(IButterfliesQuantityService));
            Butterfly butterfly = (Butterfly)validationContext.ObjectInstance;
            if (butterfly.ButterflyFamily != null)
            {
                int? quantity = service.GetButterflyFamilyQuantity(butterfly.ButterflyFamily.Value);
                int? sumQuantity = quantity + butterfly.Quantity;
                if (sumQuantity > _maxAmount)
                {
                    return new ValidationResult(string.Format("The store can hold up to {0} butterflies of the same variety currently there are {1}", _maxAmount, quantity));
                }
                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}