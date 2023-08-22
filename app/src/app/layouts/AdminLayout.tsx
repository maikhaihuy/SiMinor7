import { Box, ThemeProvider, createTheme } from '@mui/material';
import GlobalDialog from 'components/dialogs/GlobalDialog';
import GlobalAlert from 'components/GlobalAlert';
import { Outlet } from 'react-router';
import { useNavigate } from 'react-router-dom';
import { appThemes } from 'styles/theme';
import themeTypography from 'styles/typography';

const AdminLayout = () => {
  const rootThemes = appThemes();
  const adminThemes = createTheme({ ...rootThemes, typography: { ...themeTypography(), fontFamily: 'Roboto' } });
  
  return (
    <ThemeProvider theme={adminThemes}>
      <GlobalDialog />
      <GlobalAlert />
      <Outlet />
    </ThemeProvider>
  );
};

export default AdminLayout;
