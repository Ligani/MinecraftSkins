import React from 'react';

export default function CatalogFilters({ searchQuery, setSearchQuery, availableOnly, setAvailableOnly, onFilterChange }) {
  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value);
    onFilterChange();
  };

  const handleToggleChange = (e) => {
    setAvailableOnly(e.target.checked);
    onFilterChange(); 
  };

  return (
    <div className="catalog-controls">
      <input 
        type="text" 
        className="search-input"
        placeholder="Поиск по названию скина..."
        value={searchQuery}
        onChange={handleSearchChange}
      />

      <label className="filter-container">
        <input 
          type="checkbox" 
          className="switch-input"
          checked={availableOnly}
          onChange={handleToggleChange}
        />
        <div className="switch-slider"></div>
        <span className="filter-label">Только доступные скины</span>
      </label>
    </div>
  );
}