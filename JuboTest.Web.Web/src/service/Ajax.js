import axios from "axios";

const Ajax = {
  get: function (url, onSuccess, onError, onFinish) {    
    axios
      .get(`${process.env.REACT_APP_API_ROOT}${url}`)
      .then(response => {
        if (response.data.hasOwnProperty('success')) {
          if (response.data.success) {
              if (onSuccess) onSuccess(response.data.data);
          }
          else {
            if (onError) onError(response.data.message);
          }
        }
        else {
          console.log(response.data);
          if (onError) onError('Unknown data.');
        }
      })
      .catch(err => {
        console.log(err);
        if (onError) onError(err.message);
      })
      .then(() => {
        if (onFinish) onFinish();
      });
  },

  post: function (url, data, onSuccess, onError, onFinish) {
    axios
      .post(`${process.env.REACT_APP_API_ROOT}${url}`, data)
      .then(response => {
        if (response.data.hasOwnProperty('success')) {
          if (response.data.success) {
              if (onSuccess) onSuccess(response.data.data);
          }
          else {
            if (onError) onError(response.data.message);
          }
        }
        else {
          console.log(response.data);
          if (onError) onError('Unknown data.');
        }
      })
      .catch(err => {
        console.log(err);
        if (onError) onError(err.message);
      })
      .then(() => {
        if (onFinish) onFinish();
      });
  },
}

export default Ajax;