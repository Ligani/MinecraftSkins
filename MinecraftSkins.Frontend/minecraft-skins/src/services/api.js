import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json',
    'X-User-Id': '550e8400-e29b-41d4-a716-446655440000'
  }
});

api.interceptors.response.use(
  (response) => response,
  (error) => {
    let message = "Произошла ошибка";
    if (error.response && error.response.data) {
      const data = error.response.data;

      if (data.detail) {
        message = data.detail; 
      } 
      else if (data.errors) {
        message = Object.values(data.errors).flat().join('\n');
      }
      else if (data.title) {
        message = data.title;
      }
    } else if (error.request) {
      message = "Нет ответа от сервера. Проверьте подключение.";
    } else {
      message = error.message;
    }
    return Promise.reject(message);
  }
);

export default api;