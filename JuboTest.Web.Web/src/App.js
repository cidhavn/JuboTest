import React, { useState, useEffect } from 'react';
import './css/App.css';
import { Box, Grid } from '@mui/material'; 
import PatientList from './page/PatientList';
import OrderList from './page/OrderList';

function App() {
  const [selectPatientNo, setSelectPatientNo] = useState('');

  return (
    <Box>
      <Grid container>
        <Grid item xs={4} p={2} sx={{borderRight: '1px solid #c1c1c1'}}>
          <PatientList onSelectPatient={(patientNo) => setSelectPatientNo(patientNo)} />
        </Grid>
        <Grid item xs={8} p={2}>
          <OrderList patientNo={selectPatientNo} />
        </Grid>
      </Grid>
    </Box>
  );
}

export default App;
