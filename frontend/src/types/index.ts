export interface Car {
  id?: string;
  brand: string;
  model: string;
  banType: string;
  odometer: number;
  odometerUnit: string;
  price: number;
  currency: string;
  year: string;
  engine: number;
  credit?: boolean;
  barter?: boolean;
  images: string[];
  city: string;
  dates: string;
}

export interface FilterOptions {
  brand?: string;
  model?: string;
  city?: string;
  priceMin?: number;
  priceMax?: number;
  currency?: string;
  banType?: string;
  yearMin?: number;
  yearMax?: number;
  credit?: boolean;
  barter?: boolean;
  condition?: 'all' | 'new' | 'used';
}

export interface FilterData {
  brands: string[];
  cities: string[];
  banTypes: string[];
  years: number[];
}

