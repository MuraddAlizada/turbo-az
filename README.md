# Turbo.Az - React + TypeScript

Bu layihə React + TypeScript frontend istifadə edir. Məlumatlar frontend-də saxlanılır, backend lazım deyil.

## Struktur

```
Tubo.az-main/
├── frontend/          # React + TypeScript
│   ├── src/
│   │   ├── components/
│   │   ├── services/
│   │   ├── data/
│   │   ├── types/
│   │   ├── App.tsx
│   │   └── main.tsx
│   ├── public/
│   └── package.json
└── img/               # Şəkillər
```

## Quraşdırma

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend `http://localhost:5173` ünvanında işləyəcək.

## Xüsusiyyətlər

- ✅ 66 avtomobil məlumatı
- ✅ Filtrləmə funksionallığı (Marka, Model, Şəhər, Qiymət, Valyuta, Ban növü, İl)
- ✅ Kredit/Barter filtrləri
- ✅ Yeni/Sürülmüş seçimi
- ✅ "Daha çox göstər" funksionallığı
- ✅ Responsive dizayn

## Texnologiyalar

- **Frontend**: React 18, TypeScript, Vite, Tailwind CSS
- **Package Manager**: npm

