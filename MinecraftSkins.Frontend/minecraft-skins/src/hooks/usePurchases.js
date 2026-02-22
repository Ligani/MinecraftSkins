import { useState, useEffect } from 'react';
import purchaseService from '../services/purchaseService';

export function usePurchases() {
  const [purchases, setPurchases] = useState([]);
  const [fromDate, setFromDate] = useState(''); 
  const [toDate, setToDate] = useState('');     
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);   

  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      setLoading(true);

      purchaseService.getPaged({
        from: fromDate || null,
        to: toDate || null,
        pageNumber: page,
        pageSize: 10
      })
      .then(data => {
        setPurchases(data.items || []);
        setTotalPages(data.totalPages || 1);
      })
      .catch(err => {
        console.error("Ошибка загрузки покупок:", err);
      })
      .finally(() => {
        setLoading(false);
      });
    }, 300);

    return () => clearTimeout(delayDebounceFn);
    
  }, [fromDate, toDate, page]); 


  return { purchases, fromDate, setFromDate, toDate, setToDate, page, setPage, totalPages, loading };
}