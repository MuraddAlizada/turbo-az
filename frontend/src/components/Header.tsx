import React from 'react';

const Header: React.FC = () => {
  return (
    <>
      <div className="bg-[#f6f7fa] text-[15px] hidden md:block">
        <div className="max-w-[1010px] m-auto px-3 py-2">
          <div className="container m-auto flex justify-between items-center font-arial">
            <div className="text-[#8d94ad] space-x-[25px]">
              <a href="https://tap.az" target="_blank" rel="noopener noreferrer" className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">Tap.az</a>
              <a href="https://bina.az" target="_blank" rel="noopener noreferrer" className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">Bina.az</a>
              <a href="https://boss.az" target="_blank" rel="noopener noreferrer" className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">Boss.az</a>
            </div>
            <div className="flex gap-4">
              <div>
                <span className="cursor-default">Dəstək:</span>
                <a href="tel:+994125264747" className="text-black hover:text-[#ca1016] transition ease-in-out duration-300">(012)526-47-47</a>
              </div>
              <a href="#" className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">Yardım</a>
              <a href="#" className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">RU</a>
              <button className="text-[#8d94af] hover:text-[#ca1016] transition ease-in-out duration-300">
                <i className="fa-solid fa-heart mr-1"></i>Seçilmişlər
              </button>
            </div>
          </div>
        </div>
      </div>

      <header className="bg-[#ca1016] py-[10px]">
        <div className="max-w-[1010px] m-auto">
          <nav className="flex flex-col gap-4 items-center md:flex-row container md:justify-between m-auto px-3 font-arial">
            <menu className="md:flex text-center text-white items-center">
              <li className="text-white text-[18px] font-[700] md:pr-9">
                <a href="/">TURBO.AZ</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">Bütün elanlar</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">Dilerlər</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">Avtokatoloq</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">Moto</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">Ehtiyyat hissələri ve aksesuarlar</a>
              </li>
              <li className="lg:px-[10px] my-1 md:my-0 px-[5px] text-[15px]">
                <a href="#" className="hover:text-[#f2c2c4]">İcarə</a>
              </li>
            </menu>
            <button className="bg-[#7ed321] h-10 w-[105px] rounded-[7px] text-white flex justify-center items-center cursor-pointer transition ease-in-out duration-300 hover:bg-[#85c01f]">
              <i className="fa-regular fa-circle-plus"></i>
              <span className="ml-1">Yeni elan</span>
            </button>
          </nav>
        </div>
      </header>
    </>
  );
};

export default Header;

