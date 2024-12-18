﻿using Grand.Infrastructure.Models;

namespace Grand.Web.Models.Catalog;

public class PopularProductTagsModel : BaseModel
{
    #region Methods

    public virtual int GetFontSize(ProductTagModel productTag)
    {
        var itemWeights = Tags.Select(tag => tag.ProductCount).Select(dummy => (double)dummy).ToList();
        var stdDev = StdDev(itemWeights, out var mean);

        return GetFontSize(productTag.ProductCount, mean, stdDev);
    }

    #endregion

    #region Utilities

    protected virtual int GetFontSize(double weight, double mean, double stdDev)
    {
        var factor = weight - mean;

        if (factor != 0 && stdDev != 0) factor /= stdDev;

        return factor > 2 ? 150 :
            factor > 1 ? 120 :
            factor > 0.5 ? 100 :
            factor > -0.5 ? 90 :
            factor > -1 ? 85 :
            factor > -2 ? 80 :
            75;
    }

    protected virtual double Mean(IEnumerable<double> values)
    {
        double sum = 0;
        var count = 0;

        foreach (var d in values)
        {
            sum += d;
            count++;
        }

        return sum / count;
    }

    protected virtual double StdDev(IEnumerable<double> values, out double mean)
    {
        mean = Mean(values);
        double sumOfDiffSquares = 0;
        var count = 0;

        foreach (var d in values)
        {
            var diff = d - mean;
            sumOfDiffSquares += diff * diff;
            count++;
        }

        return Math.Sqrt(sumOfDiffSquares / count);
    }

    #endregion

    #region Properties

    public int TotalTags { get; set; }

    public IList<ProductTagModel> Tags { get; set; } = new List<ProductTagModel>();

    #endregion
}