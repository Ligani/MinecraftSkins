import React from 'react';

export default function SkinCard({ skin, onBuy, disabled }) {
  return (
    <div className="skin-card" style={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
      <div style={{ display: 'flex', flexDirection: 'column', gap: '5px', flexGrow: 1 }}>
        
        <h3 style={{ margin: '0 0 10px 0', fontSize: '1.3rem' }}>{skin.name}</h3>
        
        <div className="skin-price" style={{ fontSize: '1.1rem' }}>
          ${skin.finalPriceUsd?.toLocaleString()} USD
        </div>
      </div>

      <div style={{ 
        display: 'flex', 
        justifyContent: 'space-between', 
        alignItems: 'center', 
        marginTop: '25px', 
        paddingTop: '15px',
        borderTop: '1px solid rgba(255,255,255,0.1)'
      }}>
        <span 
          className="skin-status" 
          style={{ 
            color: skin.isAvailable ? 'var(--success-color)' : '#ef4444', 
            fontWeight: 'bold' 
          }}
        >
          {skin.isAvailable ? '● Доступен' : '● Продан'}
        </span>

        <button 
          onClick={() => onBuy(skin.id)}
          disabled={!skin.isAvailable || disabled}
          style={{ marginTop: 0, padding: '8px 20px' }}
        >
          {disabled ? '...' : 'Купить'}
        </button>
      </div>
      
    </div>
  );
}