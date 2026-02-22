import React from 'react';
import SkinCard from './SkinCard';

export default function SkinGrid({ skins, loading, onBuy, isSubmitting }) {
  if (loading) return <p>Загрузка каталога...</p>;

  if (skins.length === 0) {
    return <p style={{ color: 'var(--text-muted)' }}>Скины не найдены.</p>;
  }

  return (
    <div className="catalog-grid">
      {skins.map(skin => (
        <SkinCard 
          key={skin.id} 
          skin={skin} 
          onBuy={onBuy} 
          disabled={isSubmitting} 
        />
      ))}
    </div>
  );
}