import React, { useState, useEffect } from 'react';
import { Car, FilterOptions } from './types';
import { carService } from './services/carService';
import CarCard from './components/CarCard';
import FilterSection from './components/FilterSection';
import Header from './components/Header';

const App: React.FC = () => {
  const [cars, setCars] = useState<Car[]>([]);
  const [filteredCars, setFilteredCars] = useState<Car[]>([]);
  const [filters, setFilters] = useState<FilterOptions>({});
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const itemsPerPage = 8;

  useEffect(() => {
    loadCars();
  }, []);

  useEffect(() => {
    // Məlumatlar yüklənəndən sonra filtrləri tətbiq et
    if (cars.length > 0) {
      applyFilters();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [filters]);

  const loadCars = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await carService.getAll();
      setCars(data);
      setFilteredCars(data);
    } catch (error: any) {
      console.error('Error loading cars:', error);
      setError('Məlumatlar yüklənə bilmədi');
      setCars([]);
      setFilteredCars([]);
    } finally {
      setLoading(false);
    }
  };

  const applyFilters = async () => {
    try {
      setError(null);
      const data = await carService.getAll(filters);
      setFilteredCars(data);
      setCurrentPage(1);
    } catch (error: any) {
      console.error('Error applying filters:', error);
      setError('Filtrlər tətbiq edilə bilmədi');
    }
  };

  const handleLoadMore = () => {
    setCurrentPage(prev => prev + 1);
  };

  const displayedCars = filteredCars.slice(0, currentPage * itemsPerPage);
  const hasMore = displayedCars.length < filteredCars.length;

  if (loading && cars.length === 0) {
    return (
      <div className="bg-gray-100 min-h-screen flex items-center justify-center">
        <div className="text-[#ca1016] text-xl">Yüklənir...</div>
      </div>
    );
  }

  return (
    <div className="bg-gray-100 min-h-screen">
      <Header />
      <FilterSection onFilterChange={setFilters} />
      
      {error && (
        <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded relative max-w-[1010px] m-auto mt-4" role="alert">
          <strong className="font-bold">Xəta: </strong>
          <span className="block sm:inline">{error}</span>
        </div>
      )}

      <div className="bg-[#f1f3f7] border">
        <div className="max-w-[1010px] m-auto py-[20px]">
          <h1 className="container m-auto font-arial font-[700] text-[16px] px-3 uppercase">Salonların vıp elanları</h1>
        </div>
      </div>

      <div className="bg-[#f6f7fa]">
        <div className="max-w-[1010px] m-auto py-[20px]">
          <h1 className="container m-auto font-arial font-[700] text-[18px] pl-3 pr-[10px] py-4 inline text-black">
            Avtomobil almaq
          </h1>
          <span className="text-[#8d94ad] text-[15px] font-[400]">
            {filteredCars.length} elan
          </span>
        </div>

        {displayedCars.length === 0 && !loading ? (
          <div className="max-w-[1010px] m-auto py-10 text-center">
            <p className="text-gray-600 text-lg">Avtomobil tapılmadı</p>
            {error && (
              <p className="text-gray-500 text-sm mt-2">{error}</p>
            )}
          </div>
        ) : (
          <div className="max-w-[1010px] m-auto">
            <section className="mt-[29px]">
              <div className="container m-auto">
                <div className="grid grid-cols-1 gap-x-4 gap-y-8 md:grid-cols-2 lg:grid-cols-4">
                  {displayedCars.map((car, index) => (
                    <CarCard key={car.id || `car-${index}`} car={car} />
                  ))}
                </div>
              </div>
            </section>
          </div>
        )}

        {hasMore && (
          <div className="flex justify-center p-5">
            <button
              onClick={handleLoadMore}
              className="bg-[#7ed321] hover:bg-[#85c01f] transition ease-in-out duration-300 px-2 py-3 rounded-[7px] text-white"
            >
              Daha çox göstər<i className="fa-solid fa-chevron-right pl-[5px]"></i>
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default App;

