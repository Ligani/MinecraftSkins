import api from './api';

const exchangeRateService = {
  async getRate() {
    const response = await api.get('/exchange-rate');
    return response.data;
  }
};

export default exchangeRateService;