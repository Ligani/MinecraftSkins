import React from 'react';

export default function PurchaseFilters({ fromDate, setFromDate, toDate, setToDate, onReset }) {
  return (
    <div className="catalog-controls">
      <div className="filter-group">
        <div className="date-input-wrapper">
          <label className="filter-label">От:</label>
          <input 
            type="date" 
            className="search-input" 
            value={fromDate}
            onChange={(e) => setFromDate(e.target.value)}
          />
        </div>
        
        <div className="date-input-wrapper">
          <label className="filter-label">До:</label>
          <input 
            type="date" 
            className="search-input"
            value={toDate}
            onChange={(e) => setToDate(e.target.value)}
          />
        </div>
        
        <button className="btn-secondary" onClick={onReset}>
          Сбросить фильтры
        </button>
      </div>
    </div>
  );
}