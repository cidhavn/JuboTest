import React, { useState, useEffect } from 'react';
import Ajax from '../service/Ajax';
import { TableContainer, Table, TableHead, TableBody, TableRow, TableCell } from '@mui/material';
import { Dialog, DialogTitle, DialogContent, DialogActions } from '@mui/material';
import { Button, TextField, Snackbar, Alert } from '@mui/material';

const PatientList = (props) => {
  const { onSelectPatient } = props;
  const [patientData, setPatientData] = useState([]);
  const [patientLoading, setPatientLoading] = useState(false);
  const [alertSuccessShow, setAlertSuccessShow] = useState(false);
  const [alertSuccessMessage, setAlertSuccessMessage] = useState('');
  const [alertErrorShow, setAlertErrorShow] = useState(false);
  const [alertErrorMessage, setAlertErrorMessage] = useState('');
  const [editShow, setEditShow] = useState(false);
  const [editCreate, setEditCreate] = useState(false);
  const [editLoading, setEditLoading] = useState(false);
  const [editNo, setEditNo] = useState('');
  const [editName, setEditName] = useState('');

  useEffect(() => {
    load();
  }, []); 

  const load = () => {    
    if (patientLoading) return;

    patientData.length = 0;
    setPatientLoading(true);

    Ajax.get(
      '/api/patient/list',
      (data) => {
        if (data && data.length > 0) data.map(item => patientData.push(item));
      },
      (errorMessage) => {
        console.log(errorMessage);
        showErrorMessage(errorMessage);
      },
      () => {
        setPatientLoading(false);
      });
  };

  const add = () => {
    setEditShow(true);
    setEditCreate(true);
  }

  const edit = (no, name) => {
    setEditShow(true);
    setEditCreate(false);
    setEditNo(no);
    setEditName(name);
  }

  const cancel = () => {
    setEditShow(false);
    setEditNo('');
    setEditName('');
  }

  const save = () => {
    if (editLoading) return;

    setEditLoading(true);

    if (editCreate) {
      Ajax.post(
        '/api/patient/create',
        {
          no: editNo,
          name: editName,
          gender: 'F',
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
        '/api/patient/edit',
        {
          no: editNo,
          name: editName,
          gender: 'F',
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

  const selectPatient = (patientNo) => {
    if (onSelectPatient) onSelectPatient(patientNo);
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
        <Button color="primary" variant="outlined" onClick={() => add()}>新增病歷</Button>
      </div>
      <TableContainer>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell component="th">No</TableCell>
              <TableCell component="th">Name</TableCell>
              <TableCell component="th"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {patientData.length === 0 &&
              <TableRow>
                <TableCell colSpan={3} align={'center'}>No Data</TableCell>
              </TableRow>
            }
            {patientData.length > 0 &&
              patientData.map((item, index) => (
                <TableRow key={item.no}>
                  <TableCell>{item.no}</TableCell>
                  <TableCell>{item.name}</TableCell>
                  <TableCell>
                    <Button color="info" variant="outlined" sx={{ mr: 2 }} onClick={() => edit(item.no, item.name)}>編輯</Button>
                    <Button color="info" variant="outlined" sx={{ mr: 2 }} onClick={() => selectPatient(item.no)}>選擇</Button>
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
        <DialogTitle>{editCreate ? '新增' : '編輯'}病歷</DialogTitle>
        <DialogContent>
          <TextField
              required
              disabled={editCreate === false}
              variant="standard"              
              label="No"
              value={editNo}
              onChange={(e) => { setEditNo(e.target.value); }}              
              sx={{ mr: 2 }}
            />
            <TextField
              required
              variant="standard"
              label="Name"
              value={editName}
              onChange={(e) => { setEditName(e.target.value); }}
              sx={{ mr: 2 }}
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

export default PatientList;