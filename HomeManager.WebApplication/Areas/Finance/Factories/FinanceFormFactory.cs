﻿using HomeManager.Models.Entities;
using HomeManager.Models.Entities.Finance;
using HomeManager.Models.Enums;
using HomeManager.Models.Interfaces.Factories.Finance;
using HomeManager.Models.Interfaces.Finance;
using HomeManager.WebApplication.Areas.Finance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = HomeManager.Models.Entities.Finance.Type;

namespace HomeManager.WebApplication.Areas.Finance.Factories
{
    public class FinanceFormFactory : IFinanceFormFactory
    {
        private readonly IPaymentService _paymentService;
        private readonly ICategoryService _categoryService;
        private readonly ITypeService _typeService;
        private readonly IStatusService _statusService;
        private readonly ITemplateService _paymentTemplateService;

        public FinanceFormFactory(IPaymentService paymentService, ICategoryService categoryService, ITypeService typeService, IStatusService statusService, ITemplateService paymentTemplateService)
        {
            _paymentService = paymentService;
            _categoryService = categoryService;
            _typeService = typeService;
            _statusService = statusService;
            _paymentTemplateService = paymentTemplateService;
        }

        public async Task<string> GetCreateForm(User user, int typeId)
        {
            try
            {
                var result = "";
                var type = await _typeService.GetById(user, typeId);
                var status = await _statusService.GetByTypeId(user, typeId);
                var category = await _categoryService.GetAll(user);

                if (status == null)
                {
                    return result;
                }

                var model = MakeElements(new Payment(), "Create", status, category);

                result += MakeResult(model, "Create", type, new Payment());

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<string> GetCreateFromTemplateForm(User user, int templateId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetEditForm(User user, int paymentId)
        {
            try
            {
                var result = "";
                var payment = await _paymentService.GetById(user, paymentId);
                var types = await _typeService.GetAll(user);
                var status = await _statusService.GetAll(user);
                var category = await _categoryService.GetAll(user);

                if (types == null || status == null)
                {
                    return result;
                }

                var typeElement = "<div class='form-group form-floating created'>" +
                           "<select name='fk_TypeId' class ='form-control' id='dropdownTypeEditPayment'>";

                foreach (var type in types)
                {
                    typeElement += "<option value='" + type.Id + "'";
                    if (payment.fk_TypeId == type.Id)
                    {
                        typeElement += "selected";
                    }
                    typeElement += ">" + type.Name + "</option>";
                }

                typeElement += "</select>" +
                           "<label for='fk_TypeId' class='control-label'>Type</label>" +
                           "</div >";

                var model = MakeElements(payment, "Edit", status, category);

                var currentType = payment.Type;

                result = typeElement;

                result += MakeResult(model, "Edit", currentType, payment);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string MakeResult(PaymentModel model, string method, Type type, Payment payment)
        {
            var result = "<br class='created' />" + model.Date + "<br class='created' />" + model.Description + "<br class='created' />" + model.Amount;

            var extraInput = type.ExtraInput != null ? string.Join(',', type.ExtraInput) : "";

            result += "<div class='created'>" + model.AdvancedAmount;

            if (extraInput.Contains(PaymentExtraInput.Extra_Amount.ToString()))
            {
                result += " | " + model.AdvancedExtraCosts + "</div> <div class='created' id='divExtraCosts" + method + "PaymentFinance' style='display: none'> <br class='created' />" + model.ExtraCosts  + model.AddExtraCost + "</div>";
            }

            if (type.EndTaxType == TaxType.Gross)
            {
                result += "<div class='created' id='divAdvancedAmount" + method + "PaymentFinance' style='display: none'>" + model.Amount_Gross + "<br class='created' />" + model.Amount_Net + "</div>";

            }
            else if (type.EndTaxType == TaxType.Net)
            {
                result += "<div class='created' id='divAdvancedAmount" + method + "PaymentFinance' style='display: none'>" + model.Amount_Net + "<br class='created' />" + model.Amount_Gross + "</div>";
            }

            result += "<br class='created' />" + model.Tax;

            if (extraInput.Contains(PaymentExtraInput.TaxList.ToString()))
            {
                result += model.AdvancedTaxList + "<br class='created' /> <div class='created' id='divAdvancedTax" + method + "PaymentFinance' style='display: none'> <br class='created' />" + model.TaxList  + model.AddTax + "</div>";
            }

            result += "<br class='created' />" + model.Category + "<br class='created' />" + model.Status + "<br class='created' />" + model.Invoice;

            return result;
        }

        private PaymentModel MakeElements(Payment payment, string method, ICollection<Status> statuses, ICollection<Category> categories)
        {
            var result = new PaymentModel();

            result.Date = "<div class='form-group form-floating  created'>" +
                "<input name='Date' placeholder='Date' class='form-control' type='date'  id='datepickerDate" + method + "PaymentFinance' value='" + payment.Date.ToString("dd.MM.yyyy") + "' />" +
                "<label for='Date' class='control-label'>Date</label>" +
                "</div>";
            result.Description = "<div class='form-group form-floating created'>" +
                "<input name='Description' placeholder='Description' class='form-control' id='inputDescription" + method + "PaymentFinance' value='" + payment.Description + "' />" +
                "<label for='Description'>Description</label>" +
                "</div>";
            result.Amount = "<div class='form-group form-floating created'>" +
                "<input name='Amount' placeholder='Amount' class='form-control' id='inputAmount" + method + "PaymentFinance' value='" + payment.Amount + "' />" +
                "<label for='Amount' class='control-label'>Total Amount</label>" +
                "</div>";
            result.Amount_Gross = "<div class='form-group form-floating created'>" +
                "<input name='Amount_Gross' placeholder='Amount_Gross' class='form-control' id='inputAmountGross" + method + "PaymentFinance' value='" + payment.Amount_Gross + "' />" +
                "<label for='Amount_Gross' class='control-label'>Amount Gross</label>" +
                "</div>";
            result.Amount_Net = "<div class='form-group form-floating created'>" +
                "<input name='Amount_Net' placeholder='Amount_Net' class='form-control' id='inputAmountNet" + method + "PaymentFinance' value='" + payment.Amount_Net + "' />" +
                "<label for='Amount_Net' class='control-label'>Amount Net</label>" +
                "</div>";
            result.Tax = "<div class='row g-3 form-group created'>" +
                "<div class='col form-floating created'>" +
                "<input name='Tax' placeholder='Tax' class='form-control' id='inputTax" + method + "PaymentFinance' value='" + payment.Tax + "' />" +
                "<label for='Tax' class='control-label'>Tax</label>" +
                "</div>" +
                "<div class='col form-floating created'>" +
                "<input name='Amount_Tax' placeholder='Amount_Tax' class='form-control' id='inputAmountTax" + method + "PaymentFinance' value='" + payment.Amount_Tax + "' />" +
                "<label for='Amount_Tax' class='control-label'>Amount Tax</label>" +
                "</div>" +
                "</div>";
            if (payment.Amount_ExtraCosts != null)
            {
                var countExtraCosts = payment.Amount_TaxList.Count();

                for (int i = 0; i < countExtraCosts; i++)
                {
                    if (i > 0)
                    {
                        result.ExtraCosts += "<br class='created' />";
                    }
                    result.ExtraCosts += "<div class='row g-3 form-group created'>" +
                "<div class='col form-floating'>" +
                "<input name='Description_ExtraCosts' placeholder='Description_ExtraCosts' class='form-control' id='inputDescriptionExtraCosts" + method + "PaymentFinance' value='" + payment.Description_ExtraCosts[i] + "' />" +
                "<label for='Description_ExtraCosts' class='control-label'>Description</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='Amount_ExtraCosts' placeholder='Amount_ExtraCosts' class='form-control' id='inputAmountExtraCosts" + method + "PaymentFinance' value='" + payment.Amount_ExtraCosts[i] + "' />" +
                "<label for='Amount_ExtraCosts' class='control-label'>Amount</label>" +
                "</div>" +
                "</div>";
                }

                result.AdvancedExtraCosts = "<a href='#' class='link-dark created' id='linkAdvancedExtraCosts" + method + "PaymentFinance'>Extra Cost</a>";
                result.AddExtraCost = "<a href='#' class='link-dark created' id='linkAddExtraCost" + method + "PaymentFinance'>Add Extra Cost</a>";
            }
            else
            {
                result.ExtraCosts += "<div class='row g-3 form-group created'>" +
                "<div class='col form-floating'>" +
                "<input name='Description_ExtraCosts' placeholder='Description_ExtraCosts' class='form-control' id='inputDescriptionExtraCosts" + method + "PaymentFinance' value='' />" +
                "<label for='Description_ExtraCosts' class='control-label'>Description</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='Amount_ExtraCosts' placeholder='Amount_ExtraCosts' class='form-control' id='inputAmountExtraCosts" + method + "PaymentFinance' value='' />" +
                "<label for='Amount_ExtraCosts' class='control-label'>Amount</label>" +
                "</div>" +
                "</div>";

                result.AdvancedExtraCosts = "<a href='#' class='link-dark created' id='linkAdvancedExtraCosts" + method + "PaymentFinance'>Extra Cost</a>";
                result.AddExtraCost = "<a href='#' class='link-dark created' id='linkAddExtraCost" + method + "PaymentFinance'>Add Extra Cost</a>";
            }

            if (payment.Amount_TaxList != null)
            {
                var countTaxList = payment.Amount_TaxList.Count();

                for (int i = 0; i < countTaxList; i++)
                {
                    if (i > 0)
                    {
                        result.TaxList += "<br class='created' />";
                    }
                    result.TaxList += "<div class='row g-3 form-group created advancedTax'>" +
                "<div class='col-6 form-floating'>" +
                "<input name='Description_TaxList' placeholder='Description_TaxList' class='form-control' id='inputDescriptionTaxList" + method + "PaymentFinance' value='" + payment.Description_TaxList[i] + "' />" +
                "<label for='Description_TaxList' class='control-label'>Description</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='TaxList' placeholder='TaxList' class='form-control' id='inputTaxList" + method + "PaymentFinance' value='" + payment.TaxList[i] + "' />" +
                "<label for='TaxList' class='control-label'>Tax %</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='Amount_TaxList' placeholder='Amount_TaxList' class='form-control' id='inputAmountTaxList" + method + "PaymentFinance' value='" + payment.Amount_TaxList[i] + "' />" +
                "<label for='Amount_TaxList' class='control-label'>Amount</label>" +
                "</div>" +
                "</div>";
                }
                result.AdvancedTaxList = "<a href='#' class='link-dark created' id='linkAdvancedTaxList" + method + "PaymentFinance'>Advanced</a>";
                result.AddTax = "<a href='#' class='link-dark created' id='linkAddTax" + method + "PaymentFinance'>Add Tax</a>";
            }
            else
            {
                result.TaxList += "<div class='row g-3 form-group created advancedTax'>" +
                "<div class='col-6 form-floating'>" +
                "<input name='Description_TaxList' placeholder='Description_TaxList' class='form-control' id='inputDescriptionTaxList" + method + "PaymentFinance' value='' />" +
                "<label for='Description_TaxList' class='control-label'>Description</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='TaxList' placeholder='TaxList' class='form-control' id='inputTaxList" + method + "PaymentFinance' value='' />" +
                "<label for='TaxList' class='control-label'>Tax %</label>" +
                "</div>" +
                "<div class='col form-floating'>" +
                "<input name='Amount_TaxList' placeholder='Amount_TaxList' class='form-control' id='inputAmountTaxList" + method + "PaymentFinance' value='' />" +
                "<label for='Amount_TaxList' class='control-label'>Amount</label>" +
                "</div>" +
                "</div>";

                result.AdvancedTaxList = "<a href='#' class='link-dark created' id='linkAdvancedTaxList" + method + "PaymentFinance'>Advanced</a>";
                result.AddTax = "<a href='#' class='link-dark created' id='linkAddTax" + method + "PaymentFinance'>Add Tax</a>";
            }

            result.Invoice = "<div class='form-group created'>" +
                "<label class='control-label' for='files'>Upload Invoice</label>" +
                "<input class='form-control' name='files' type='file' id='uploadFiles" + method + "PaymentFinance' />" +
                "</div>";

            if (categories != null)
            {
                var categoryElement = "<div class='form-group form-floating created'>" +
                "<select name='fk_CategoryId' class ='form-control' id='dropdownCategory" + method + "PaymentFinance'>";
                if (payment.fk_CategoryId == null)
                {
                    categoryElement += "<option value='- 1' disabled selected>Select a Category</option>'";
                }

                foreach (var category in categories)
                {
                    categoryElement += "<option value='" + category.Id + "'";
                    if (payment.fk_CategoryId == category.Id)
                    {
                        categoryElement += "selected";
                    }
                    categoryElement += ">" + category.Name + "</option>";
                }

                categoryElement += "</select>" +
                           "<label for='fk_CategoryId' class='control-label'>Category</label>" +
                    "</div>";
                result.Category = categoryElement;
            }


            var statusElement = "<div class='form-group form-floating created'>" +
                "<select name='fk_StatusId' class ='form-control' id='dropdownStatus" + method + "PaymentFinance'>";

            if (payment.fk_StatusId == 0)
            {
                statusElement += "<option value='-1' disabled selected>Select a Status</option>";
            }

            foreach (var status in statuses)
            {
                statusElement += "<option value='" + status.Id + "'";
                if (payment.fk_StatusId == status.Id)
                {
                    statusElement += "selected";
                }
                statusElement += ">" + status.Name + "</option>";
            }

            statusElement += "</select>" +
                       "<label for='fk_StatusId' class='control-label'>Status</label>" +
                "</div>";


            result.Status = statusElement;

            result.AdvancedAmount = "<a href='#' class='link-dark created' id='linkAdvancedAmount" + method + "PaymentFinance' value='0' >Advanced</a>";

            return result;
        }
    }
}
