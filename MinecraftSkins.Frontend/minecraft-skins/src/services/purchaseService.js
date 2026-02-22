import api from './api';

const purchaseService = {
  async buy(skinId) {
    const response = await api.post('/purchases', { skinId });
    return response.data;
  },

  async getPaged(params = {}) {
    const defaultParams = {
      mineOnly: true, 
      pageNumber: 1,
      pageSize: 6,
      ...params
    };

    const response = await api.get('/purchases', { params: defaultParams });
    return response.data;
  },
};

export default purchaseService;