using System;
using FluentValidation;
using GateWays.Common.Pagination;

namespace GateWays.Common.Validator
{
    public class PaginationValidator : AbstractValidator<PaginationParams>
    {
        public PaginationValidator()
        {
            RuleFor(p => p.PageSize).GreaterThanOrEqualTo(1).WithMessage("Page size should be greater than or equal to 1")
                .LessThanOrEqualTo(20).WithMessage("Page size should be less than or equal to 20");

            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page Number should be greater than or equal to 1");
        }
    }
}
