import React from 'react';

export default function SuccessBanner({ purchase }) {
  if (!purchase) return null;

  return (
    <div className="success-banner">
      <h3>✅ Покупка успешна!</h3>
      <p><strong>ID транзакции:</strong> {purchase.id}</p>
    </div>
  );
}