# Sürətli Başlanğıc

## Problem: Avtomobillər görünmür

Bu problem backend serverinin işləməməsindən yarana bilər.

## Həll:

### 1. Backend-i işə salın:

Yeni terminal pəncərəsində:

```bash
cd backend
dotnet restore
dotnet run
```

Backend `http://localhost:5000` ünvanında işləyəcək.

### 2. Frontend-i işə salın:

Başqa bir terminal pəncərəsində:

```bash
cd frontend
npm install
npm run dev
```

Frontend `http://localhost:5173` ünvanında işləyəcək.

### 3. Yoxlayın:

- Backend işləyirsə: `http://localhost:5000/swagger` - Swagger UI açılmalıdır
- Frontend işləyirsə: `http://localhost:5173` - Sayt açılmalıdır
- Avtomobillər görünməlidir

## Xəta mesajı görürsünüzsə:

Əgər frontend-də "Backend serveri işləmir" xətası görürsünüzsə:
1. Backend-in işlədiyini yoxlayın
2. `http://localhost:5000/api/cars` URL-ə browser-də daxil olun - JSON məlumat görməlisiniz
3. CORS problemi ola bilər - backend-də CORS konfiqurasiyasını yoxlayın

