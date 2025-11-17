import { Car, FilterOptions, FilterData } from '../types';
import { carsData } from '../data/carsData';

// Backend olmadan, məlumatları frontend-də filtrləyirik
export const carService = {
  getAll: async (filters?: FilterOptions): Promise<Car[]> => {
    // Simulyasiya üçün kiçik delay
    await new Promise(resolve => setTimeout(resolve, 100));
    
    let filtered = [...carsData];

    if (filters) {
      if (filters.brand) {
        filtered = filtered.filter(c => c.brand === filters.brand);
      }

      if (filters.model) {
        filtered = filtered.filter(c => c.model === filters.model);
      }

      if (filters.city) {
        filtered = filtered.filter(c => c.city === filters.city);
      }

      if (filters.priceMin !== undefined) {
        filtered = filtered.filter(c => c.price >= filters.priceMin!);
      }

      if (filters.priceMax !== undefined) {
        filtered = filtered.filter(c => c.price <= filters.priceMax!);
      }

      if (filters.currency) {
        filtered = filtered.filter(c => c.currency === filters.currency);
      }

      if (filters.banType) {
        filtered = filtered.filter(c => c.banType === filters.banType);
      }

      if (filters.yearMin !== undefined) {
        filtered = filtered.filter(c => parseInt(c.year) >= filters.yearMin!);
      }

      if (filters.yearMax !== undefined) {
        filtered = filtered.filter(c => parseInt(c.year) <= filters.yearMax!);
      }

      if (filters.credit !== undefined) {
        filtered = filtered.filter(c => c.credit === filters.credit);
      }

      if (filters.barter !== undefined) {
        filtered = filtered.filter(c => c.barter === filters.barter);
      }

      if (filters.condition === "new") {
        filtered = filtered.filter(c => c.odometer === 0);
      } else if (filters.condition === "used") {
        filtered = filtered.filter(c => c.odometer > 0);
      }
    }

    return filtered;
  },

  getById: async (id: string): Promise<Car> => {
    await new Promise(resolve => setTimeout(resolve, 100));
    const car = carsData.find(c => c.id === id);
    if (!car) {
      throw new Error('Car not found');
    }
    return car;
  },

  getFilterOptions: async (): Promise<FilterData> => {
    await new Promise(resolve => setTimeout(resolve, 100));
    
    const brands = [...new Set(carsData.map(c => c.brand))].sort();
    const cities = [...new Set(carsData.map(c => c.city))].sort();
    const banTypes = [...new Set(carsData.map(c => c.banType))].sort();
    const years = [...new Set(carsData.map(c => parseInt(c.year)).filter(y => !isNaN(y)))].sort((a, b) => a - b);

    return {
      brands,
      cities,
      banTypes,
      years,
    };
  },
};

