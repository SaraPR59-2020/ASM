using LocalControllerProject;
using LocalDeviceProject;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementTest
{

    [TestFixture]
    public class LocalControllerTest
    {

        [Test]
        public void LocalControllerConstructor() 
        {
            LocalController localController = new LocalController();

            Assert.IsNotNull(localController.XmlReader);
            Assert.IsNotNull(localController.XmlWriter);
            Assert.IsNotNull(localController.MyAMSStream);
        }

        [Test]
        public void SendToAMS_RetursnTrue()
        {
            //Arrange
            LocalController localController = new LocalController();

            var xml = new Mock<XmlReader>();
            xml.Setup(x => x.ReadData(It.IsAny<string>())).Returns(new List<LocalDevice>());

            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            stream.Setup(x => x.Close()).Verifiable();

            localController.XmlReader = xml.Object;
            localController.MyAMSStream = stream.Object;



            //Act
            bool result = localController.SendToAMS();


            //Assert

            Assert.IsTrue(result);
        }


        [Test]
        public void SendToAMS_ReturnsFalse()
        {
            //Arrange
            LocalController localController = new LocalController();

            var xml = new Mock<XmlReader>();
            xml.Setup(x => x.ReadData(It.IsAny<string>())).Returns(new List<LocalDevice>());

            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            stream.Setup(x => x.Close()).Verifiable();

            localController.XmlReader = xml.Object;
            localController.MyAMSStream = stream.Object;



            //Act
            bool resutl = localController.SendToAMS();


            //Assert

            Assert.IsFalse(resutl);
        }


        [Test]
        public void ReceiveData_ReturnsFalse() 
        {
            //Arrange
            LocalController localController = new LocalController();



            var t = new Mock<MyTcpClient>();
            t.Setup(x => x.GetStream()).Verifiable();


            localController.MyClient = t.Object;
            var xml = new Mock<XmlWritter>();
            xml.Setup(x => x.WriteData(It.IsAny<LocalDevice>(), It.IsAny<string>())).Verifiable();

            localController.XmlWriter = xml.Object;

            var myTcp = new Mock<MyTcpListener>();
            myTcp.Setup(x => x.AcceptTcpClient()).Throws(new Exception());

            localController.MyServer = myTcp.Object;


            var stream = new Mock<MyNetworkStream>();
            stream.Setup(x => x.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Verifiable();


            localController.MyStream = stream.Object;




            //Act
            bool result = localController.ReceiveData();

            //Assert
            Assert.IsFalse(result);
        }
    }
}
