namespace Trirand.Web.Mvc
{
    using System;

    public enum SearchOperation
    {
        IsEqualTo,
        IsNotEqualTo,
        IsLessThan,
        IsLessOrEqualTo,
        IsGreaterThan,
        IsGreaterOrEqualTo,
        IsIn,
        IsNotIn,
        BeginsWith,
        DoesNotBeginWith,
        EndsWith,
        DoesNotEndWith,
        Contains,
        DoesNotContain
    }
}

