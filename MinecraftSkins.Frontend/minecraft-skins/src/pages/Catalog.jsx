import React from 'react';
import { useSkins } from '../hooks/useSkins';
import { usePurchase } from '../hooks/usePurchase';
import CatalogFilters from '../components/CatalogFilters';
import SkinGrid from '../components/SkinGrid';
import SuccessBanner from '../components/SuccessBanner';
import Pagination from '../components/Pagination'; 

export default function Catalog() {
  const { 
    skins, setSkins, page, setPage, totalPages, loading, 
    searchQuery, setSearchQuery, 
    availableOnly, setAvailableOnly 
  } = useSkins();

  const handleSuccess = (purchasedSkinId) => {
    setSkins(prevSkins => 
      prevSkins.map(skin => 
        skin.id === purchasedSkinId ? { ...skin, isAvailable: false } : skin
      )
    );
  };

  const { buySkin, lastPurchase, isSubmitting } = usePurchase(handleSuccess);

  return (
    <div className="app-container">
      <h1>Каталог скинов</h1>

      <SuccessBanner purchase={lastPurchase} />

      <CatalogFilters 
        searchQuery={searchQuery}
        setSearchQuery={setSearchQuery}
        availableOnly={availableOnly}
        setAvailableOnly={setAvailableOnly}
        onFilterChange={() => setPage(1)}
      />

      <SkinGrid 
        skins={skins} 
        loading={loading} 
        onBuy={buySkin} 
        isSubmitting={isSubmitting} 
      />

      <Pagination 
        page={page} 
        totalPages={totalPages} 
        setPage={setPage} 
      />
    </div>
  );
}