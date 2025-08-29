### 1. Suggested Features (with Users in Mind)

Since this is a **prototype for demonstration**, and you're using **users** mainly to justify private functionality, here's a practical feature set:

#### Core Features
- `Map view`: Publicly show parking spots with live status (available/occupied).
- `Spot details`: Click on a pin to see real-time occupancy info.
- `User login`: OAuth or basic JWT-based auth.
- `Favorites`: Authenticated users can save favorite parking locations.
- `Reservation system (mock)`: Allow logged-in users to “reserve” a spot for a limited time (simulate with a timer).
- `History`: Authenticated users can view their recent parking sessions.

#### Why These?
- **Public map** = accessible to all.
- **Favorites/reservations/history** = reasons to log in.
- Avoids overcomplication while providing user value and project scope.

---

### 2. Development Plan (Bullet Points)

#### 🔧 Backend First – Core Infrastructure
- [ ] Set up **PostgreSQL on AWS RDS** (Free Tier), define schema (`ParkingSpots`, `Users`, `Reservations`, `Sessions`).
- [ ] Start with `.NET 8 Web API`:
  - CRUD for `ParkingSpots`.
  - Auth endpoints (register/login).
  - Endpoints for favorites & reservations (auth required).
- [ ] Add **SignalR Hub** to broadcast spot updates to frontend.
- [ ] Create a **BackgroundService** to consume MQTT messages (from sensors/mock) and update DB + broadcast via SignalR.

#### 📡 IoT Simulation Layer
- [ ] Deploy **Mosquitto or EMQX in Docker**.
- [ ] Write a mock script (Node.js/Python) to simulate sensor data and publish to MQTT.
  - Topics like: `parking/spot/{id}/status`.

#### 🖥️ Frontend MVP
- [ ] Scaffold with **Vite + React + TypeScript**.
- [ ] Add **Leaflet + OpenStreetMap** for map display.
- [ ] Fetch initial parking data from API and plot pins.
- [ ] Subscribe to SignalR for live updates and re-render map markers.
- [ ] Add basic UI:
    - Login / Register modal
    - Spot detail panel
    - Favorites list (protected route)
    - Mock reservation flow

#### 🧪 Testing & Demo
- [ ] Run MQTT mock script to change statuses dynamically.
- [ ] Test flow: public map → login → reserve spot → see update.
- [ ] Record short demo video of:
    - Real-time spot updates.
    - Login/reserve/favorites flow.

#### 📦 Tools & Stack Summary

| Layer        | Tech                           |
|--------------|--------------------------------|
| Backend      | .NET 8 Web API, SignalR        |
| DB           | PostgreSQL (AWS RDS)           |
| Auth         | JWT Bearer tokens              |
| IoT          | Mosquitto/EMQX (Docker)        |
| Frontend     | Vite + React + Leaflet         |
| Styling      | Tailwind CSS                   |
| Deployment   | Local/Docker for demo          |

---

### 🚀 Suggested Start Order

1. **DB Schema & Models** (EF Core)
2. **Auth endpoints + middleware**
3. **MQTT Broker + Mock Publisher**
4. **Backend Ingest Service**
5. **API Endpoints + SignalR**
6. **Frontend Map UI**
7. **Live Updates via SignalR**
8. **Add User Features (Favorites/Reserve)**
9. **Final Testing & Demo Prep**

Let me know if you want to begin scaffolding files or DB schema.