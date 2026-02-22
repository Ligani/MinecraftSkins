import React from 'react';

export default function PurchaseTable({ purchases, loading }) {
  if (loading) return <p>Загрузка истории...</p>;

  return (
    <table className="purchases-table">
      <thead>
        <tr>
          <th>ID Транзакции</th>
          <th>Скин</th>
          <th>Дата покупки</th>
          <th>Уплачено (USD)</th>
          <th>Курс BTC</th>
        </tr>
      </thead>
      <tbody>
        {purchases.length === 0 ? (
          <tr>
            <td colSpan="5" style={{ textAlign: 'center', padding: '30px', color: 'var(--text-muted)' }}>
              Покупок за этот период не найдено
            </td>
          </tr>
        ) : (
          purchases.map(p => (
            <tr key={p.id}>
              <td title={p.id}>{p.id.substring(0, 8)}...</td>
              <td>{p.skinId}</td> 
              <td>{p.purchacedAt ? new Date(p.purchacedAt).toLocaleString() : '---'}</td>
              <td className="skin-price">${p.finalPrice || '---'}</td>
              <td style={{ color: 'var(--text-muted)' }}>
                {p.rate ? `$${Number(p.rate).toLocaleString()}` : '---'}
              </td> 
            </tr>
          ))
        )}
      </tbody>
    </table>
  );
}