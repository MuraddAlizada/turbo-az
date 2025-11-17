import axios from 'axios';
import { Car, FilterOptions, FilterData } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const carService = {
  getAll: async (filters?: FilterOptions): Promise<Car[]> => {
    const response = await api.get<Car[]>('/cars', { params: filters });
    return response.data;
  },

  getById: async (id: string): Promise<Car> => {
    const response = await api.get<Car>(`/cars/${id}`);
    return response.data;
  },

  getFilterOptions: async (): Promise<FilterData> => {
    const response = await api.get<FilterData>('/cars/filter-options');
    return response.data;
  },
};

export default api;

