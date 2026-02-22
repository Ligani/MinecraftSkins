import { useState, useEffect } from 'react';
import skinService from '../services/skinService';

export function useSkins(initialPage = 1) {
  const [skins, setSkins] = useState([]);
  const [page, setPage] = useState(initialPage);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);
  
  const [searchQuery, setSearchQuery] = useState('');
  const [availableOnly, setAvailableOnly] = useState(false);

  useEffect(() => {
    const delayDebounceFn = setTimeout(() => {
      setLoading(true);
      
      skinService.getPaged({ 
        pageNumber: page, 
        pageSize: 6, 
        search: searchQuery || null,
        availableOnly: availableOnly 
      })
        .then(data => {
          setSkins(data.items || []);
          setTotalPages(data.totalPages || 1);
        })
        .catch(err => {
          console.error(err);
          alert("Не удалось загрузить скины");
        })
        .finally(() => setLoading(false));
    }, 300);

    return () => clearTimeout(delayDebounceFn);
      
  }, [page, searchQuery, availableOnly]);

  return { skins, setSkins, page, setPage, totalPages, loading, searchQuery, setSearchQuery,availableOnly,setAvailableOnly 
  };
}