using TurboAz.Api.Models;

namespace TurboAz.Api.Services;

public class CarService : ICarService
{
    private readonly List<Car> _cars;

    public CarService()
    {
        _cars = InitializeCars();
    }

    public Task<IEnumerable<Car>> GetCarsAsync(CarFilterOptions? filters = null)
    {
        var query = _cars.AsQueryable();

        if (filters == null)
        {
            return Task.FromResult<IEnumerable<Car>>(query.ToList());
        }

        if (!string.IsNullOrEmpty(filters.Brand))
        {
            query = query.Where(c => c.Brand == filters.Brand);
        }

        if (!string.IsNullOrEmpty(filters.Model))
        {
            query = query.Where(c => c.Model == filters.Model);
        }

        if (!string.IsNullOrEmpty(filters.City))
        {
            query = query.Where(c => c.City == filters.City);
        }

        if (filters.PriceMin.HasValue)
        {
            query = query.Where(c => c.Price >= filters.PriceMin.Value);
        }

        if (filters.PriceMax.HasValue)
        {
            query = query.Where(c => c.Price <= filters.PriceMax.Value);
        }

        if (!string.IsNullOrEmpty(filters.Currency))
        {
            query = query.Where(c => c.Currency == filters.Currency);
        }

        if (!string.IsNullOrEmpty(filters.BanType))
        {
            query = query.Where(c => c.BanType == filters.BanType);
        }

        if (filters.YearMin.HasValue)
        {
            query = query.Where(c => int.TryParse(c.Year, out int year) && year >= filters.YearMin.Value);
        }

        if (filters.YearMax.HasValue)
        {
            query = query.Where(c => int.TryParse(c.Year, out int year) && year <= filters.YearMax.Value);
        }

        if (filters.Credit.HasValue)
        {
            query = query.Where(c => c.Credit == filters.Credit.Value);
        }

        if (filters.Barter.HasValue)
        {
            query = query.Where(c => c.Barter == filters.Barter.Value);
        }

        if (filters.Condition == "new")
        {
            query = query.Where(c => c.Odometer == 0);
        }
        else if (filters.Condition == "used")
        {
            query = query.Where(c => c.Odometer > 0);
        }

        return Task.FromResult<IEnumerable<Car>>(query.ToList());
    }

    public Task<Car?> GetCarByIdAsync(string id)
    {
        var car = _cars.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(car);
    }

    public Task<FilterData> GetFilterOptionsAsync()
    {
        var options = new FilterData
        {
            Brands = _cars.Select(c => c.Brand).Distinct().OrderBy(b => b).ToList(),
            Cities = _cars.Select(c => c.City).Distinct().OrderBy(c => c).ToList(),
            BanTypes = _cars.Select(c => c.BanType).Distinct().OrderBy(b => b).ToList(),
            Years = _cars
                .Select(c => int.TryParse(c.Year, out int year) ? year : 0)
                .Where(y => y > 0)
                .Distinct()
                .OrderBy(y => y)
                .ToList()
        };

        return Task.FromResult(options);
    }

    private List<Car> InitializeCars()
    {
        return new List<Car>
        {
            new Car { Brand = "Abarth", Model = "Seltos", BanType = "Offroader / SUV", Odometer = 30000, OdometerUnit = "km", Price = 44000, Currency = "AZN", Year = "2005", Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F11%2F11%2F19%2F41%2F5f34c8eb-5138-4d0c-b78e-8c817d98aa5f%2F52522_Mu2ZZs1LqQkRDJpgK-R_xw.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Brand = "Audi", Model = "A5", BanType = "Sedan", Odometer = 165000, OdometerUnit = "km", Price = 16000, Currency = "USD", Year = "2013", Engine = 1.5m, Credit = true, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F08%2F31%2F23%2F10%2F15%2F729c8b30-ea56-45d3-bdf7-f8a7b0e9e950%2F3015_eyJujDX3UzCcNboINLPl_g.jpg" }, City = "Masallı", Dates = "Bu gün  14:30" },
            new Car { Id = "3", Brand = "Toyota", Model = "Camry", BanType = "Sedan", Odometer = 191000, OdometerUnit = "km", Price = 17300, Currency = "AZN", Year = "2007", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F12%2F22%2F10%2F26%2F59%2F9f754715-d306-49f2-b2e2-60ab36f771e6%2F80870_wrsWRRQ-A4ySXmcF4jOpXg.jpg" }, City = "Ağdam", Dates = "Bu gün  14:30" },
            new Car { Id = "4", Brand = "Ford", Model = "Transit", BanType = "Karvan", Odometer = 11000, OdometerUnit = "km", Price = 44500, Currency = "USD", Year = "2008", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F09%2F02%2F17%2F28%2F48%2Fa0bc3142-1854-48f9-9c81-ff7d7010c1ae%2F59959_CCv7BAr0Tlz1x7lQiFAsQA.jpg" }, City = "Naftalan", Dates = "Bu gün  14:30" },
            new Car { Id = "5", Brand = "Bestune", Model = "T77", BanType = "Offroader / SUV", Odometer = 0, OdometerUnit = "km", Price = 45900, Currency = "AZN", Year = "2022", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F09%2F19%2F17%2F33%2F45%2F027df0e9-c5df-46a1-8428-2a356ee17d45%2F44832_yspkMx-Q-VQPULaAOI_MSw.jpg" }, City = "Oğuz", Dates = "Bu gün  14:30" },
            new Car { Id = "6", Brand = "Honggi", Model = "H9", BanType = "Sedan", Odometer = 0, OdometerUnit = "km", Price = 94000, Currency = "AZN", Year = "2005", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F04%2F19%2F15%2F59%2F38%2F716b705f-e564-4d55-995a-7762e6881f4c%2F5883_vLXDlraa-zAkIkuXUdl05w.jpg" }, City = "Qax", Dates = "Bu gün  14:30" },
            new Car { Id = "7", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 141622, OdometerUnit = "km", Price = 19500, Currency = "AZN", Year = "2015", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F29%2F15%2F18%2F37%2F46fbb03a-0cd9-4409-bbc6-03023e588329%2F67200_jmTFoB8S36kCqexBERvuhA.jpg" }, City = "Gədəbəy", Dates = "Bu gün  14:30" },
            new Car { Id = "8", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 129000, OdometerUnit = "km", Price = 25900, Currency = "AZN", Year = "2017", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F29%2F18%2F08%2F03%2F20fc5e31-1f10-4b83-ade8-2d513db1c746%2F67209_O84rpgQMDkok2-nwStiHpg.jpg" }, City = "Gəncə", Dates = "Bu gün  14:30" },
            new Car { Id = "9", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 46000, OdometerUnit = "km", Price = 21200, Currency = "USD", Year = "2020", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F11%2F11%2F19%2F52%2F19%2F56291fd3-453f-426f-a911-3ca4f2e26362%2F17663_stveq78AA4WtPo3oyvVbHA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "10", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 24945, OdometerUnit = "km", Price = 18500, Currency = "AZN", Year = "2015", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F19%2F14%2F59%2F27%2F530eae46-d07d-4abc-bacb-de36d3088f0f%2F64922_Iu72BGd2C_OFe3WxVk-qGQ.jpg" }, City = "Ağcabədi", Dates = "Bu gün  14:30" },
            new Car { Id = "11", Brand = "Kia", Model = "Seltos", BanType = "Offroader / SUV", Odometer = 30000, OdometerUnit = "km", Price = 40000, Currency = "AZN", Year = "2005", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F10%2F20%2F20%2F19%2F37%2Ff0d40936-67d4-47b2-8f88-39e95c546ae3%2F57379_yzPNLn7TAwj6RDByWKgvOA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "12", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 192000, OdometerUnit = "km", Price = 23500, Currency = "AZN", Year = "2014", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F30%2F15%2F48%2F00%2F097e698b-cbe7-4539-ab25-9a13a2a753fd%2F75247_xWTTEaAst4sLzUzIUkthPg.jpg" }, City = "Ağdaş", Dates = "Bu gün  14:30" },
            new Car { Id = "13", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 20500, OdometerUnit = "km", Price = 42000, Currency = "AZN", Year = "2015", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F22%2F20%2F08%2F29%2F52caefc7-1570-4c7c-a077-fd1f816203cb%2F64916_MHdPUxu1eQ3kLAtY4hEzYg.jpg" }, City = "Ağsu", Dates = "Bu gün  14:30" },
            new Car { Id = "14", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 30000, OdometerUnit = "km", Price = 40000, Currency = "AZN", Year = "2005", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F06%2F18%2F51%2F58%2Fc79abd63-a69c-48eb-b071-37506b868231%2F57686_CzbKH3fGBbBdGyq3D-qstQ.jpg" }, City = "Ağdaş", Dates = "Bu gün  14:30" },
            new Car { Id = "15", Brand = "Ford", Model = "Sedan", BanType = "Sedan", Odometer = 122000, OdometerUnit = "km", Price = 40000, Currency = "AZN", Year = "2015", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F28%2F22%2F00%2F43%2F37fc61b4-f0e5-46c2-af9d-80f0b15b12c1%2F67189_ktr5R2gTZl44Bt3WKDgx2w.jpg" }, City = "Bərdə", Dates = "Bu gün  14:30" },
            new Car { Id = "16", Brand = "Ford", Model = "Sedan", BanType = "Sedan", Odometer = 122000, OdometerUnit = "km", Price = 40000, Currency = "AZN", Year = "2015", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F28%2F22%2F00%2F43%2F37fc61b4-f0e5-46c2-af9d-80f0b15b12c1%2F67189_ktr5R2gTZl44Bt3WKDgx2w.jpg" }, City = "Bərdə", Dates = "Bu gün  14:30" },
            new Car { Id = "17", Brand = "Ford", Model = "Sedan", BanType = "Sedan", Odometer = 12000, OdometerUnit = "km", Price = 40000, Currency = "EUR", Year = "2015", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F22%2F15%2F32%2F14%2F772966ba-5666-475a-a372-f12bf29a6393%2F78864_UB3_bVH_R5hoI0WN_uPsDQ.jpg" }, City = "Bərdə", Dates = "Bu gün  14:30" },
            new Car { Id = "18", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 10000, OdometerUnit = "km", Price = 33000, Currency = "AZN", Year = "2005", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F22%2F21%2F06%2F21%2F7fc2989d-5606-4bba-84d5-91a7c1e3c11b%2F86601_iBRqpWaL3FFHzWvi-4ZGNQ.jpg" }, City = "Yevlax", Dates = "Bu gün  14:30" },
            new Car { Id = "19", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 30000, OdometerUnit = "km", Price = 40000, Currency = "EUR", Year = "2005", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F12%2F19%2F55%2F07%2F9e2ccf81-d944-4b2f-8e63-a63bb368dfeb%2F8520_gtkNyAHKA6hUsNhuKMZOxw.jpg" }, City = "Göyçay", Dates = "Bu gün  14:30" },
            new Car { Id = "20", Brand = "Ford", Model = "Fusion", BanType = "Sedan", Odometer = 50000, OdometerUnit = "km", Price = 12000, Currency = "USD", Year = "2005", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F06%2F13%2F55%2F32%2Facf4c052-5a2e-4bd0-a0df-e28ecc595151%2F11719_z5jP8sffJjJ_qsKGBXbQsw.jpg" }, City = "İmişli", Dates = "Bu gün  14:30" },
            new Car { Id = "21", Brand = "Mercedes", Model = "A 140", BanType = "Hetçbek", Odometer = 50000, OdometerUnit = "km", Price = 12000, Currency = "USD", Year = "2005", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F29%2F19%2F39%2F48%2Fbae4dd1b-0f4e-4894-9e9b-7d4ed873ab27%2F67204_3_Uie01KEoSx4Ki0QWYwnA.jpg" }, City = "Bərdə", Dates = "Bu gün  14:30" },
            new Car { Id = "22", Brand = "Chevrolet", Model = "Gatsby", BanType = "Kabriolet", Odometer = 16900, OdometerUnit = "km", Price = 126000, Currency = "USD", Year = "1986", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F09%2F16%2F12%2F50%2F42%2Febf8e8a9-4b9e-42d1-bb0c-725555a056e1%2F54915_qtOrHDVrjjW3-GNgDyN8vg.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "23", Brand = "Jaguar", Model = "F-Type R", BanType = "Kupe", Odometer = 23000, OdometerUnit = "km", Price = 53000, Currency = "USD", Year = "2014", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F07%2F08%2F11%2F07%2F31%2Fd5fce961-38e1-47dd-8cf2-0eafc47ddb85%2F48187_u9isVaR_H7KHZwjuTS97bA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "24", Brand = "Mercedes", Model = "E 430", BanType = "Kupe", Odometer = 20200, OdometerUnit = "km", Price = 22000, Currency = "AZN", Year = "2000", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F30%2F18%2F47%2F06%2F92202a2f-1f27-453f-a37e-c03b988d1187%2F49755_qZklKVXHkSfjWtrA_iHn_A.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "25", Brand = "C.Moto", Model = "CM250R- HY", BanType = "Motosiklet", Odometer = 0, OdometerUnit = "km", Price = 5400, Currency = "AZN", Year = "2023", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F31%2F16%2F42%2F46%2Fb60abf3c-aff7-450d-b793-bf93ceff3a06%2F15660_QvgVQpBtt9j4-9bpnj5N8Q.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "26", Brand = "Toyota", Model = "Sienna", BanType = "Minivan", Odometer = 22000, OdometerUnit = "km", Price = 51400, Currency = "USD", Year = "2020", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2022%2F12%2F29%2F00%2F03%2F08%2F2ac930d3-3d5a-4b19-94fb-b63850ff5693%2F15334_aQbJ3Ea3Oz_WMOo_hHKCAg.jpg" }, City = "Horadiz", Dates = "Bu gün  14:30" },
            new Car { Id = "27", Brand = "Toyota", Model = "Prius", BanType = "Liftbek", Odometer = 188293, OdometerUnit = "km", Price = 13900, Currency = "EUR", Year = "2020", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F24%2F14%2F33%2F40%2F0f9b3be6-4ae5-417a-bcf1-db0f39927d4e%2F33485_P0YBt9TmP9pEv29rCuhWLg.jpg" }, City = "İsmayıllı", Dates = "Bu gün  14:30" },
            new Car { Id = "28", Brand = "Porsche", Model = "Panamera GTS", BanType = "Liftbek", Odometer = 188293, OdometerUnit = "km", Price = 55000, Currency = "EUR", Year = "2020", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F02%2F01%2F01%2F26%2F17%2Ffb77794e-9d37-424e-944d-3b233bc03467%2F12003_RjupQqZAh9kZFu-IaHqJ7g.jpg" }, City = "Şahbuz", Dates = "Bu gün  14:30" },
            new Car { Id = "29", Brand = "Paz", Model = "672", BanType = "Avtobus", Odometer = 50000, OdometerUnit = "km", Price = 12000, Currency = "USD", Year = "2005", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F23%2F21%2F49%2F47%2F18227380-ca87-4ecc-8497-3c972bce2db1%2F42127_wKRyc3J6-wogJe-WZMY-ug.jpg" }, City = "İmişli", Dates = "Bu gün  14:30" },
            new Car { Id = "30", Brand = "Ferrari", Model = "California", BanType = "Rodster", Odometer = 20000, OdometerUnit = "km", Price = 135000, Currency = "USD", Year = "2017", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F12%2F28%2F15%2F40%2F48%2Fb18d5c9e-58d7-4e2e-9bba-1c29cbce9940%2F61425_r8Og48iy5Jr9GvOTtAnnyQ.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "31", Brand = "Mercedes", Model = "E 280", BanType = "Sedan", Odometer = 129000, OdometerUnit = "km", Price = 19700, Currency = "AZN", Year = "2009", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F31%2F21%2F26%2F41%2Fa8168d7c-d02e-495f-8f01-69fcdc5e3e03%2F11997_tG1Qr36Aqkf171mt0oZG7Q.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "32", Brand = "Mercedes", Model = "E 200", BanType = "Sedan", Odometer = 315000, OdometerUnit = "km", Price = 14000, Currency = "AZN", Year = "2001", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F02%2F01%2F00%2F51%2F52%2F5638fb6b-7249-4f46-abd0-aa1efc451203%2F15643_AMLCTJrCWlvBJ6SXlF8fpg.jpg" }, City = "Sumqayıt", Dates = "Bu gün  14:30" },
            new Car { Id = "33", Brand = "Mercedes", Model = "C 240", BanType = "Sedan", Odometer = 451000, OdometerUnit = "km", Price = 9500, Currency = "AZN", Year = "1997", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F02%2F01%2F00%2F50%2F33%2Fd092e0d3-04c1-4af7-a730-c240c35f5f7d%2F15649_bymZBq9rqL0M4TLjYtXR4A.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "34", Brand = "Mercedes", Model = "A 170", BanType = "Hetçbek", Odometer = 199000, OdometerUnit = "km", Price = 10200, Currency = "AZN", Year = "2006", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F30%2F20%2F06%2F23%2F76cef983-6b3b-47b8-a58d-dfb3cf469b5d%2F75221_xUr0C4Z--56j6RIijVBWVA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "35", Brand = "Mercedes", Model = "Actros 1841", BanType = "Dartqı", Odometer = 1300000, OdometerUnit = "km", Price = 67500, Currency = "AZN", Year = "2008", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F11%2F09%2F08%2F03%2F05%2Fcabbc910-8026-456f-a642-62ab99fc313e%2F10369_bNrqvfrJMy63hz0DwhXPcg.jpg" }, City = "Lənkəran", Dates = "Bu gün  14:30" },
            new Car { Id = "36", Brand = "Mercedes", Model = "G 63 AMG", BanType = "Offroader / SUV", Odometer = 28000, OdometerUnit = "km", Price = 90200, Currency = "AZN", Year = "2016", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F31%2F20%2F36%2F16%2Ffb49abb0-fdf6-4595-87b0-27bdd3c3d226%2F12009_q1FoKTnpPA3uE3dtRcZZUA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "37", Brand = "Mercedes", Model = "200 D", BanType = "Sedan", Odometer = 552000, OdometerUnit = "km", Price = 5200, Currency = "AZN", Year = "1990", Credit = false, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F31%2F19%2F25%2F15%2F3aa9741d-4cc3-4ea0-9c3f-66ae5de0516b%2F6316_s0p7GRbRLVyaXdPugbuHeA.jpg" }, City = "Saatlı", Dates = "Bu gün  14:30" },
            new Car { Id = "38", Brand = "Mercedes", Model = "0403", BanType = "Avtobus", Odometer = 700000, OdometerUnit = "km", Price = 110000, Currency = "AZN", Year = "2000", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F21%2F15%2F25%2F13%2F02798cb4-1129-4629-93af-b6dccb089966%2F78886_JqpeH6a5xab8QHW6E7dXKg.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "39", Brand = "Mercedes", Model = "GLC 300 Coupe", BanType = "Kupe", Odometer = 7000, OdometerUnit = "km", Price = 72000, Currency = "USD", Year = "2021", Credit = true, Engine = 1.5m, Barter = true, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F12%2F12%2F32%2F34%2Fef66aacc-979f-4824-9c38-4151faad457e%2F45824__18habOQ883XXQctAU0TAg.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "40", Brand = "Mercedes", Model = "AMG GT 53", BanType = "Sedan", Odometer = 0, OdometerUnit = "km", Price = 210000, Currency = "EUR", Year = "2023", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F11%2F24%2F15%2F43%2F27%2Ff071d240-bc97-412c-a94a-bd3d676a51b2%2F23828_xAXUnAgmfcF8tuCOsfkz8Q.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "41", Brand = "Mercedes", Model = "170 V", BanType = "Kupe", Odometer = 82000, OdometerUnit = "km", Price = 500000, Currency = "AZN", Year = "1938", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F10%2F27%2F14%2F39%2F36%2F923ef17d-e1f9-4149-9e94-f925cf7661b7%2F42871_umKrn-lPv1BfkzuD5arziA.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "42", Brand = "Jaguar", Model = "XF", BanType = "Sedan", Odometer = 95000, OdometerUnit = "km", Price = 35000, Currency = "AZN", Year = "2014", Credit = false, Barter = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F28%2F12%2F48%2F02%2Fe5c1b65a-c97f-4f94-ab4d-fcac497b20b6%2F67186_iILaSiss0FC-yQLMTjYR9A.jpg" }, City = "Sumqayıt", Dates = "Bu gün  14:30" },
            new Car { Id = "43", Brand = "Jaguar", Model = "S-Type", BanType = "Sedan", Odometer = 190000, OdometerUnit = "km", Price = 17000, Currency = "AZN", Year = "2007", Credit = false, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2023%2F01%2F30%2F20%2F01%2F36%2Fb8325e8f-b0b1-4423-8163-59ab513c0445%2F49759_qjtKYlmSfvuA_6JFNFa8AQ.jpg" }, City = "Gəncə", Dates = "Bu gün  14:30" },
            new Car { Id = "44", Brand = "Jaguar", Model = "XF", BanType = "Sedan", Odometer = 9500, OdometerUnit = "km", Price = 58000, Currency = "USD", Year = "2019", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2021%2F02%2F04%2F11%2F55%2F52%2Ff3ddd4c9-1600-4d6d-b34f-5853f3c1ff71%2F19864_8RtK5APm1te6PSMZ3o2MLg.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "45", Brand = "Jaguar", Model = "F-Type R,", BanType = "Sedan", Odometer = 500, OdometerUnit = "km", Price = 60000, Currency = "EUR", Year = "2020", Credit = true, Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/f710x568/2022%2F12%2F31%2F11%2F03%2F22%2Fb774f69d-3dc8-45d8-bf74-7fe3b1e68012%2F16508_ZEyGXYpx-MxvNSzE3M3s7A.jpg" }, City = "Bakı", Dates = "Bu gün  14:30" },
            new Car { Id = "46", Brand = "Acura", Model = "MDX", BanType = "Offroader / SUV", Odometer = 500, OdometerUnit = "km", Price = 60000, Currency = "EUR", Year = "2020", Engine = 1.5m, Images = new List<string> { "https://turbo.azstatic.com/uploads/full/2023%2F01%2F31%2F13%2F41%2F14%2F30be4e7e-c9ac-455d-8616-616f096d6da7%2F71593_f9yNl7lW4FO-spgOift6dw.jpg" }, City = "Mingecevir", Dates = " Bu gün  14:30" }
        };
    }
}

