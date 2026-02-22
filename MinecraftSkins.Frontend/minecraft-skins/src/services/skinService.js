import api from './api';

const skinService = {

  async getPaged(params = {}) {
    const defaultParams = {
      availableOnly: false, 
      search: null,         
      pageNumber: 1,       
      pageSize: 10,       
      ...params             
    };
    const response = await api.get('/skins', { params: defaultParams });
    return response.data; 
  },
};

export default skinService;