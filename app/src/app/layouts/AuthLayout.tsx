import { Box, createTheme } from '@mui/material';
import Container from '@mui/material/Container';
import CssBaseline from '@mui/material/CssBaseline';
import LocalizationMenu from 'features/language/components/LocalizationMenu';
import { Outlet } from 'react-router';

interface IProps extends React.PropsWithChildren {
  id: string;
}

const AuthPageLayout = () => {
  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Outlet />
      <Box
        sx={{
          position: 'absolute',
          top: 24,
          right: 24
        }}
      >
        <LocalizationMenu />
      </Box>
    </Container>
  );
};

export default AuthPageLayout;
