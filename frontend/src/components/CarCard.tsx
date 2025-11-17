import React from 'react';
import { Car } from '../types';

interface CarCardProps {
  car: Car;
}

const CarCard: React.FC<CarCardProps> = ({ car }) => {
  return (
    <article className="flex flex-col bg-white rounded-lg shadow-md hover:shadow-xl transition-shadow duration-300 overflow-hidden cursor-pointer group">
      <div className="max-h-[200px] min-h-[200px] rounded-t-md overflow-hidden bg-gray-200 relative">
        <img
          alt={`${car.brand} ${car.model}`}
          className="object-cover h-full w-full group-hover:scale-110 transition-transform duration-300"
          src={car.images[0]}
          onError={(e) => {
            (e.target as HTMLImageElement).src = 'https://via.placeholder.com/300x200?text=Şəkil+Yoxdur';
          }}
        />
        <div className="absolute top-2 right-2 flex gap-1">
          {car.credit && (
            <span className="bg-green-500 text-white text-xs px-2 py-1 rounded">Kredit</span>
          )}
          {car.barter && (
            <span className="bg-blue-500 text-white text-xs px-2 py-1 rounded ml-1">Barter</span>
          )}
        </div>
      </div>
      <div className="flex flex-col flex-1 p-4">
        <h3 className="text-[20px] font-[700] text-[#ca1016] mb-2">
          {car.price.toLocaleString()} {car.currency}
        </h3>
        <span className="text-[16px] font-semibold text-gray-800 mb-1">
          {car.brand} {car.model}
        </span>
        <span className="text-[14px] text-gray-600 mb-2">
          {car.year}, {car.engine} L, {car.odometer.toLocaleString()} {car.odometerUnit}
        </span>
        <div className="text-[13px] text-[#8d94ad] mt-auto">
          <span>{car.city}, {car.dates}</span>
        </div>
      </div>
    </article>
  );
};

export default CarCard;

