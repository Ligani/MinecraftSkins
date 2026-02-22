import { useState } from 'react';
import purchaseService from '../services/purchaseService';

export function usePurchase(onSuccess) {
  const [lastPurchase, setLastPurchase] = useState(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const buySkin = async (skinId) => {
    setIsSubmitting(true);
    setLastPurchase(null);
    
    try {
      const data = await purchaseService.buy(skinId);
      setLastPurchase(data);
      
      if (onSuccess) {
        onSuccess(skinId);
      }
      
    } catch (err) {
     alert(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  return { buySkin, lastPurchase, isSubmitting };
}