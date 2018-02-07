using System;
namespace Trirand.Web.Mvc
{
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
