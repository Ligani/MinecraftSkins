import React from 'react';

export default function Pagination({ page, totalPages, setPage }) {
  if (totalPages <= 1) return null;

  return (
    <div className="pagination">
      <button onClick={() => setPage(p => p - 1)} disabled={page <= 1}>Назад</button>
      <span>Страница {page} из {totalPages}</span>
      <button onClick={() => setPage(p => p + 1)} disabled={page >= totalPages}>Вперед</button>
    </div>
  );
}