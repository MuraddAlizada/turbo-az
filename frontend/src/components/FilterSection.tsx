import React, { useState, useEffect } from 'react';
import { FilterOptions, FilterData } from '../types';
import { carService } from '../services/carService';
import { carsData } from '../data/carsData';

interface FilterSectionProps {
  onFilterChange: (filters: FilterOptions) => void;
}

const FilterSection: React.FC<FilterSectionProps> = ({ onFilterChange }) => {
  const [filterData, setFilterData] = useState<FilterData | null>(null);
  const [filters, setFilters] = useState<FilterOptions>({});
  const [selectedCondition, setSelectedCondition] = useState<'all' | 'new' | 'used'>('all');
  const [creditActive, setCreditActive] = useState(false);
  const [barterActive, setBarterActive] = useState(false);
  const [models, setModels] = useState<string[]>([]);

  useEffect(() => {
    const loadFilterData = async () => {
      try {
        const data = await carService.getFilterOptions();
        setFilterData(data);
      } catch (error) {
        console.error('Error loading filter data:', error);
      }
    };
    loadFilterData();
  }, []);

  useEffect(() => {
    if (filters.brand) {
      // Get models for selected brand directly from data
      const brandCars = carsData.filter(c => c.brand === filters.brand);
      const uniqueModels = [...new Set(brandCars.map(c => c.model))].sort();
      setModels(uniqueModels);
    } else {
      setModels([]);
    }
  }, [filters.brand]);

  const handleFilterChange = (key: keyof FilterOptions, value: any) => {
    const newFilters = { ...filters };
    
    // Əgər value boşdursa və ya undefined-dırsa, filtri sil
    if (value === '' || value === undefined || value === null) {
      delete newFilters[key];
    } else {
      newFilters[key] = value;
    }
    
    setFilters(newFilters);
    // Filtrləri dərhal tətbiq et
    onFilterChange(newFilters);
  };

  const handleBrandChange = (brand: string) => {
    const newFilters = { ...filters };
    
    if (brand === '' || brand === undefined || brand === null) {
      delete newFilters.brand;
      delete newFilters.model; // Reset model when brand is cleared
    } else {
      newFilters.brand = brand;
      delete newFilters.model; // Reset model when brand changes
    }
    
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  const resetFilters = () => {
    const emptyFilters: FilterOptions = {};
    setFilters(emptyFilters);
    setSelectedCondition('all');
    setCreditActive(false);
    setBarterActive(false);
    setModels([]);
    onFilterChange(emptyFilters);
  };

  return (
    <div className="bg-[#ebedf3] p-[20px]">
      <div className="max-w-[1010px] m-auto">
        <form className="container m-auto text-[15px] flex flex-col items-center md:flex-row justify-center gap-4">
          <select
            id="marka"
            value={filters.brand || ''}
            onChange={(e) => {
              handleBrandChange(e.target.value);
            }}
            className="h-[46px] max-w-[235px] w-full text-[#9ca3af] cursor-pointer outline-none rounded-[7px] border focus:border-[#8d94ad] p-2"
          >
            <option value="">Marka</option>
            {filterData?.brands.map((brand) => (
              <option key={brand} value={brand}>{brand}</option>
            ))}
          </select>

          <select
            id="model"
            value={filters.model || ''}
            onChange={(e) => handleFilterChange('model', e.target.value || undefined)}
            disabled={!filters.brand || models.length === 0}
            className="h-[46px] max-w-[235px] w-full text-[#9ca3af] cursor-pointer outline-none rounded-[7px] border focus:border-[#8d94ad] p-2 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            <option value="">Model</option>
            {models.length > 0 ? (
              models.map((model) => (
                <option key={model} value={model}>{model}</option>
              ))
            ) : (
              filters.brand && <option value="" disabled>Modellər yüklənir...</option>
            )}
          </select>

          <div className="max-w-[235px] flex border rounded-[7px] border-[#dfe3e9] bg-white text-[#8d96b5] cursor-pointer">
            <div
              className={`px-4 py-[11px] rounded-tl-[7px] rounded-bl-[7px] flex items-center justify-center ${
                selectedCondition === 'all' ? 'bg-[#ca1016] text-white' : ''
              }`}
              onClick={() => {
                setSelectedCondition('all');
                handleFilterChange('condition', undefined);
              }}
            >
              Hamısı
            </div>
            <div
              className={`px-4 py-[11px] border-[#dfe3e9] border-x flex items-center justify-center ${
                selectedCondition === 'new' ? 'bg-[#ca1016] text-white' : ''
              }`}
              onClick={() => {
                setSelectedCondition('new');
                handleFilterChange('condition', 'new');
              }}
            >
              Yeni
            </div>
            <div
              className={`px-4 py-[11px] flex items-center justify-center ${
                selectedCondition === 'used' ? 'bg-[#ca1016] text-white' : ''
              }`}
              onClick={() => {
                setSelectedCondition('used');
                handleFilterChange('condition', 'used');
              }}
            >
              Sürülmüş
            </div>
          </div>

          <select
            id="city"
            value={filters.city || ''}
            onChange={(e) => handleFilterChange('city', e.target.value || undefined)}
            className="h-[46px] max-w-[235px] w-[100%] text-[#8d96b5] cursor-pointer outline-none rounded-[7px] border focus:border-[#8d94ad] p-2"
          >
            <option value="">Şəhər</option>
            {filterData?.cities.map((city) => (
              <option key={city} value={city}>{city}</option>
            ))}
          </select>
        </form>

        <div className="container m-auto text-[15px] flex flex-col items-center mt-3 md:flex-row justify-center gap-4">
          <div className="h-[46px] max-w-[235px] w-full flex text-[#8d96b5] cursor-pointer outline-none rounded-[7px] border focus:border-[#8d94ad] bg-white">
            <input
              type="number"
              placeholder="Qiymət, min."
              value={filters.priceMin !== undefined ? filters.priceMin : ''}
              onChange={(e) => {
                const value = e.target.value;
                handleFilterChange('priceMin', value && !isNaN(parseFloat(value)) ? parseFloat(value) : undefined);
              }}
              className="h-[46px] w-[50%] outline-none focus:border-[#8d94ad] p-2 rounded-tl-[7px] rounded-bl-[7px]"
            />
            <input
              type="number"
              placeholder="maks."
              value={filters.priceMax !== undefined ? filters.priceMax : ''}
              onChange={(e) => {
                const value = e.target.value;
                handleFilterChange('priceMax', value && !isNaN(parseFloat(value)) ? parseFloat(value) : undefined);
              }}
              className="h-[46px] w-[50%] outline-none border-l-[1px] focus:border-[#8d94ad] p-2 rounded-tr-[7px] rounded-br-[7px]"
            />
          </div>

          <div className="h-[46px] max-w-[235px] w-[100%] rounded-[7px] flex justify-between">
            <select
              value={filters.currency || ''}
              onChange={(e) => handleFilterChange('currency', e.target.value || undefined)}
              className="min-w-[82px] rounded-[7px] outline-none border focus:border-[#8d94ad] p-2 flex justify-between text-black"
            >
              <option value="">Valyuta</option>
              <option value="AZN">AZN</option>
              <option value="USD">USD</option>
              <option value="EUR">EUR</option>
            </select>
            <div
              className={`w-[60px] border rounded-[7px] flex justify-center items-center cursor-pointer transition ease-in-out duration-300 hover:border-[#8080808f] ${
                creditActive ? 'bg-[#ca1016] text-white' : 'bg-white text-black'
              }`}
              onClick={() => {
                const newValue = !creditActive;
                setCreditActive(newValue);
                handleFilterChange('credit', newValue ? true : undefined);
              }}
            >
              Kredit
            </div>
            <div
              className={`w-[60px] border rounded-[7px] flex justify-center items-center cursor-pointer transition ease-in-out duration-300 hover:border-[#8080808f] ${
                barterActive ? 'bg-[#ca1016] text-white' : 'bg-white text-black'
              }`}
              onClick={() => {
                const newValue = !barterActive;
                setBarterActive(newValue);
                handleFilterChange('barter', newValue ? true : undefined);
              }}
            >
              Barter
            </div>
          </div>

          <select
            value={filters.banType || ''}
            onChange={(e) => handleFilterChange('banType', e.target.value || undefined)}
            className="h-[46px] cursor-pointer outline-none text-[#8d94ad] max-w-[235px] w-full rounded-[7px] border p-2 focus:border focus:border-[#8080808f]"
          >
            <option value="">Ban növü</option>
            {filterData?.banTypes.map((banType) => (
              <option key={banType} value={banType}>{banType}</option>
            ))}
          </select>

          <div className="max-w-[235px] w-full h-[46px] bg-white flex text-[#8d96b5] rounded-[7px]">
            <select
              value={filters.yearMin || ''}
              onChange={(e) => {
                const value = e.target.value;
                handleFilterChange('yearMin', value ? parseInt(value) : undefined);
              }}
              className="w-[50%] outline-none border-r-[#8d94ad] rounded-tl-[7px] rounded-bl-[7px] focus:border-[#8d94ad] p-2"
            >
              <option value="">İl, min.</option>
              {filterData?.years.map((year) => (
                <option key={year} value={year}>{year}</option>
              ))}
            </select>
            <select
              value={filters.yearMax || ''}
              onChange={(e) => {
                const value = e.target.value;
                handleFilterChange('yearMax', value ? parseInt(value) : undefined);
              }}
              className="w-[50%] outline-none border-l-[1px] rounded-tr-[7px] rounded-br-[7px] focus:border-[#8d94ad] p-2"
            >
              <option value="">maks.</option>
              {filterData?.years.map((year) => (
                <option key={year} value={year}>{year}</option>
              ))}
            </select>
          </div>
        </div>

        <div className="container m-auto text-[15px] flex flex-col items-center md:flex-row justify-between px-3 mt-5">
          <div>
            <span className="text-black">Bu gün: </span>
            <a href="#" className="text-[#4c88f9]"> 935 yeni elan</a>
          </div>
          <div className="gap-5 mt-4 md:mt-0 md:space-x-[15px] flex items-center">
            <span
              className="text-[#8d94ad] cursor-pointer"
              onClick={resetFilters}
            >
              Sıfırla
            </span>
            <span className="text-[#ca1016] cursor-pointer">
              Daha çox filtr <i className="fa-solid fa-caret-down"></i>
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default FilterSection;

