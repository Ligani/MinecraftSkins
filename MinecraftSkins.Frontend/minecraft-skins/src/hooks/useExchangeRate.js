import { useState, useEffect } from 'react';
import exchangeRateService from '../services/exchangeRateService';

export function useExchangeRate() {
  const [rate, setRate] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    setLoading(true);
    exchangeRateService.getRate()
      .then(data => {
        const currentRate = typeof data === 'object' && data.rate ? data.rate : data;
        setRate(currentRate);
      })
      .catch(err => {
        console.error("Курс BTC временно недоступен:", err);
      })
      .finally(() => setLoading(false));
  }, []); 

  return { rate, loading };
}