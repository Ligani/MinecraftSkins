import React from 'react';
import { usePurchases } from '../hooks/usePurchases';
import PurchaseFilters from '../components/PurchaseFilters';
import PurchaseTable from '../components/PurchaseTable';
import Pagination from '../components/Pagination';

export default function MyPurchases() {
  const {
    purchases, loading, page, setPage, totalPages, 
    fromDate, setFromDate, toDate, setToDate 
  } = usePurchases();

  const handleReset = () => {
    setFromDate('');
    setToDate('');
    setPage(1);
  };

  const changeFromDate = (val) => { setFromDate(val); setPage(1); };
  const changeToDate = (val) => { setToDate(val); setPage(1); };

  return (
    <div className="app-container">
      <h1>Мои покупки (История транзакций)</h1>

      <PurchaseFilters 
        fromDate={fromDate} 
        setFromDate={changeFromDate}
        toDate={toDate} 
        setToDate={changeToDate}
        onReset={handleReset}
      />

      <div style={{ marginTop: '40px' }}>
        <PurchaseTable purchases={purchases} loading={loading} />
      </div>

      <Pagination 
        page={page} 
        totalPages={totalPages} 
        setPage={setPage} 
      />
    </div>
  );
}