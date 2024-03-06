import React, { useState, useEffect } from 'react';
import Ajax from '../service/Ajax';
import { TableContainer, Table, TableHead, TableBody, TableRow, TableCell } from '@mui/material';
import { Dialog, DialogTitle, DialogContent, DialogActions } from '@mui/material';
import { Button, TextField, Snackbar, Alert } from '@mui/material';

const OrderList = (props) => {
  const { patientNo } = props;
  const [orderData, setOrderData] = useState([]);
  const [orderLoading, setOrderLoading] = useState(false);
  const [alertSuccessShow, setAlertSuccessShow] = useState(false);
  const [alertSuccessMessage, setAlertSuccessMessage] = useState('');
  const [alertErrorShow, setAlertErrorShow] = useState(false);
  const [alertErrorMessage, setAlertErrorMessage] = useState('');
  const [editShow, setEditShow] = useState(false);
  const [editCreate, setEditCreate] = useState(false);
  const [editLoading, setEditLoading] = useState(false);
  const [editId, setEditId] = useState('');
  const [editMessage, setEditMessage] = useState('');

  useEffect(() => {
    load();
  }, [patientNo]);

  const hasSelectPatient = () => patientNo ? true : false;

  const load = () => {
    if (hasSelectPatient() === false) return;
    if (orderLoading) return;

    orderData.length = 0;
    setOrderLoading(true);

    Ajax.get(
      `/api/order/list/${patientNo}`,
      (data) => {
        if (data && data.length > 0) data.map(item => orderData.push(item));
      },
      (errorMessage) => {
        console.log(errorMessage);
        showErrorMessage(errorMessage);
      },
      () => {
        setOrderLoading(false);
      });
  };

  const add = () => {
    setEditShow(true);
    setEditCreate(true);
  }

  const edit = (id, message) => {
    setEditShow(true);
    setEditCreate(false);
    setEditId(id);
    setEditMessage(message);
  }

  const cancel = () => {
    setEditShow(false);
    setEditId('');
    setEditMessage('');
  }

  const save = () => {
    if (editLoading) return;

    setEditLoading(true);

    if (editCreate) {
      Ajax.post(
        '/api/order/create',
        {
          patientNo: patientNo,
          message: editMessage,
        },
        (data) => {
          showSuccessMessage('新增成功');
          cancel();
          load();
        },
        (errorMessage) => {
          console.log(errorMessage);
          showErrorMessage(errorMessage);
        },
        () => {
          setEditLoading(false);
        });
    }
    else {
      Ajax.post(
        '/api/order/edit',
        {
          id: editId,
          message: editMessage,
        },
        (data) => {
          showSuccessMessage('編輯成功');
          cancel();
          load();
        },
        (errorMessage) => {
          console.log(errorMessage);
          showErrorMessage(errorMessage);
        },
        () => {
          setEditLoading(false);
        });
    }
  }

  const showSuccessMessage = (message) => {
    setAlertSuccessShow(true);
    setAlertSuccessMessage(message);
  }

  const closeSuccessMessage = () => {
    setAlertSuccessShow(false);
    setAlertSuccessMessage('');
  }

  const showErrorMessage = (message) => {
    setAlertErrorShow(true);
    setAlertErrorMessage(message);
  }

  const closeErrorMessage = () => {
    setAlertErrorShow(false);
    setAlertErrorMessage('');
  }

  return (
    <div>
      <div>
        <Button color="primary" variant="outlined" disabled={hasSelectPatient() === false} onClick={() => add()}>新增醫囑</Button>
      </div>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell component="th">{patientNo} 醫囑</TableCell>
              <TableCell component="th"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {orderData.length === 0 &&
              <TableRow>
                <TableCell colSpan={3} align={'center'}>{hasSelectPatient() ? 'No Data' : '尚未選擇病歷'}</TableCell>
              </TableRow>
            }
            {orderData.length > 0 &&
              orderData.map((item, index) => (
                <TableRow key={item.id}>
                  <TableCell>
                    <div style={{marginBottom:'5px', color:'gray'}}>{(new Date(item.createTime)).toLocaleString()}</div>
                    <p style={{whiteSpace:'pre-line'}}>{item.message}</p>
                  </TableCell>
                  <TableCell>
                    <Button color="info" variant="outlined" onClick={() => edit(item.id, item.message)}>編輯</Button>
                  </TableCell>
                </TableRow>
              ))
            }
          </TableBody>
        </Table>
      </TableContainer>
      <Dialog
        open={editShow}
        onClose={cancel}>
        <DialogTitle>{editCreate ? '新增' : '編輯'}醫囑</DialogTitle>
        <DialogContent>
          <TextField
              required
              autoFocus
              variant="standard"
              multiline={true}
              rows="5"
              value={editMessage}
              onChange={(e) => { setEditMessage(e.target.value); }}
              sx={{width:'350px'}}
            />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => cancel()}>Cancel</Button>
          <Button onClick={() => save()}>Save</Button>
        </DialogActions>
      </Dialog>
      <Snackbar open={alertSuccessShow} autoHideDuration={2000} onClose={() => closeSuccessMessage()}>
        <Alert severity="success" variant="filled" sx={{ width: '100%' }}>{alertSuccessMessage}</Alert>
      </Snackbar>
      <Snackbar open={alertErrorShow} autoHideDuration={10000} onClose={() => closeErrorMessage()}>
        <Alert severity="error" variant="filled" sx={{ width: '100%' }}>{alertErrorMessage}</Alert>
      </Snackbar>
    </div>
  );
};

export default OrderList;
