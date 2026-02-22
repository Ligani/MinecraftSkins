import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Catalog from './pages/Catalog';
import MyPurchases from './pages/MyPurchases';
import { useExchangeRate } from './hooks/useExchangeRate'; 

function App() {
  const { rate, loading } = useExchangeRate(); 
  return (
    <Router>
      <div className="app-container">
  <nav className="navbar">
    <div className="nav-links">
      <Link to="/" className="nav-link">Каталог скинов</Link>
      <Link to="/my-purchases" className="nav-link">Мои покупки</Link>
    </div>
    <strong>{loading ? 'Загрузка...' : `Курс BTC: $${rate}`}</strong>
  </nav>
        <hr/>
        <main>
          <Routes>
            <Route path="/" element={<Catalog />} />
            <Route path="/my-purchases" element={<MyPurchases />} />
            <Route path="*" element={<h2>Страница не найдена</h2>} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;