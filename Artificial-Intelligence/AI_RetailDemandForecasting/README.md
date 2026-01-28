# Retail Demand Forecasting (Time Series AI) - Batch Mode + Searchable Dropdowns

## Added in this zip
- Store and Item dropdowns are **searchable** (type-to-filter) using Tom Select (Bootstrap 5 theme).
- Item list still auto-filters by Store, and the searchable list refreshes when Store changes.

## Dataset
Place `train.csv` into:
`/Data/train.csv`

Schema:
`date,store,item,sales`

## Run
1. Open in Visual Studio 2022
2. Restore NuGet packages
3. Ensure `Data/train.csv` exists (set Copy to Output Directory = Copy if newer)
4. Press F5
5. Go to `/Forecast`
